using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameData
{
    /// <summary>
    /// JSON 문자열을 정의 목록으로 역직렬화한다. Newtonsoft.Json 을 사용하므로
    /// 제네릭 List&lt;T&gt; 를 직접 읽을 수 있고, 래퍼(DataFile) 클래스가 필요 없다.
    ///
    /// 입력은 최상위 배열을 기대한다.
    ///   [ { "id": "fire_slash", ... }, ... ]
    /// 읽기 시점은 지연된다 — 생성자에 문자열을 직접 주거나, 파일/StreamingAssets 를
    /// 읽는 Func&lt;string&gt; 을 줄 수 있다.
    /// </summary>
    /// <typeparam name="T">생성할 데이터 타입.</typeparam>
    public sealed class JsonDataLoader<T> : IDataLoader<T> where T : IData
    {
        private readonly Func<string> _read;
        private readonly JsonSerializerSettings _settings;

        /// <summary>JSON 문자열을 직접 받는다.</summary>
        public JsonDataLoader(string json, JsonSerializerSettings settings = null)
            : this(() => json, settings) { }

        /// <summary>읽기를 지연하는 공급자를 받는다(파일/네트워크 등).</summary>
        public JsonDataLoader(Func<string> read, JsonSerializerSettings settings = null)
        {
            _read = read ?? throw new ArgumentNullException(nameof(read));
            _settings = settings;
        }

        public IReadOnlyList<T> Load()
        {
            string json = _read();
            if (string.IsNullOrWhiteSpace(json))
                return Array.Empty<T>();

            List<T> list = JsonConvert.DeserializeObject<List<T>>(json, _settings);
            return list ?? (IReadOnlyList<T>)Array.Empty<T>();
        }
    }
}
