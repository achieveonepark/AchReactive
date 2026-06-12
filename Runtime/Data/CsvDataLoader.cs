using System;
using System.Collections.Generic;
using System.Text;

namespace AchReactive
{
    /// <summary>
    /// CSV 텍스트를 정의 목록으로 변환한다. 첫 행을 헤더(컬럼명)로 사용하고,
    /// 각 행을 컬럼명 → 셀 값 사전으로 만들어 ICsvMapper&lt;T&gt; 에 넘긴다.
    /// 따옴표로 감싼 필드, 따옴표 안의 콤마/개행, "" 이스케이프를 지원한다.
    /// </summary>
    /// <typeparam name="T">생성할 데이터 타입.</typeparam>
    public sealed class CsvDataLoader<T> : IDataLoader<T> where T : IData
    {
        private readonly Func<string> _read;
        private readonly ICsvMapper<T> _mapper;

        /// <summary>CSV 문자열을 직접 받는다.</summary>
        public CsvDataLoader(string csv, ICsvMapper<T> mapper)
            : this(() => csv, mapper) { }

        /// <summary>읽기를 지연하는 공급자를 받는다(파일/네트워크 등).</summary>
        public CsvDataLoader(Func<string> read, ICsvMapper<T> mapper)
        {
            _read = read ?? throw new ArgumentNullException(nameof(read));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IReadOnlyList<T> Load()
        {
            string csv = _read();
            if (string.IsNullOrWhiteSpace(csv))
                return Array.Empty<T>();

            List<List<string>> rows = Parse(csv);
            if (rows.Count < 2)
                return Array.Empty<T>();

            List<string> header = rows[0];
            var result = new List<T>(rows.Count - 1);

            for (int r = 1; r < rows.Count; r++)
            {
                List<string> cells = rows[r];
                if (IsEmptyRow(cells))
                    continue;

                var row = new Dictionary<string, string>(header.Count, StringComparer.Ordinal);
                for (int c = 0; c < header.Count; c++)
                    row[header[c]] = c < cells.Count ? cells[c] : string.Empty;

                T item = _mapper.Map(row);
                if (item != null)
                    result.Add(item);
            }

            return result;
        }

        private static bool IsEmptyRow(List<string> cells)
        {
            foreach (string cell in cells)
                if (!string.IsNullOrEmpty(cell))
                    return false;
            return true;
        }

        /// <summary>RFC 4180 스타일 CSV 파서. 행 목록(각 행은 셀 목록)을 반환한다.</summary>
        private static List<List<string>> Parse(string text)
        {
            var rows = new List<List<string>>();
            var row = new List<string>();
            var field = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];

                if (inQuotes)
                {
                    if (ch == '"')
                    {
                        if (i + 1 < text.Length && text[i + 1] == '"')
                        {
                            field.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        field.Append(ch);
                    }
                    continue;
                }

                switch (ch)
                {
                    case '"':
                        inQuotes = true;
                        break;
                    case ',':
                        row.Add(field.ToString());
                        field.Clear();
                        break;
                    case '\r':
                        // \r\n 은 \n 에서 한 번에 처리. 단독 \r 도 줄바꿈으로 취급.
                        if (i + 1 < text.Length && text[i + 1] == '\n')
                            break;
                        EndRow(rows, row, field);
                        row = new List<string>();
                        break;
                    case '\n':
                        EndRow(rows, row, field);
                        row = new List<string>();
                        break;
                    default:
                        field.Append(ch);
                        break;
                }
            }

            // 마지막 필드/행 flush
            if (field.Length > 0 || row.Count > 0)
                EndRow(rows, row, field);

            return rows;
        }

        private static void EndRow(List<List<string>> rows, List<string> row, StringBuilder field)
        {
            row.Add(field.ToString());
            field.Clear();
            rows.Add(row);
        }
    }
}
