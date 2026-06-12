using System.Collections.Generic;

namespace AchReactive
{
    /// <summary>
    /// CSV 한 행(컬럼명 → 셀 값)을 데이터 객체로 변환한다.
    /// 보통 Type Designer 코드 생성이 도메인별 구현체를 만들어 주지만, 직접 구현해도 된다.
    /// </summary>
    /// <typeparam name="T">생성할 데이터 타입.</typeparam>
    public interface ICsvMapper<out T> where T : IData
    {
        /// <summary>컬럼명으로 키가 매겨진 한 행을 받아 데이터 객체를 만든다.</summary>
        T Map(IReadOnlyDictionary<string, string> row);
    }
}
