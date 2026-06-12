using System.Collections.Generic;

namespace AchReactive
{
    /// <summary>
    /// 데이터를 어디서 읽어오든 동일하게 다루기 위한 경계.
    /// JSON / CSV / Remote / 사내 포맷 등은 이 인터페이스의 구현체로 교체된다.
    /// 사용자가 자기 포맷을 쓰고 싶으면 이 인터페이스만 구현하면 된다.
    /// </summary>
    /// <typeparam name="T">로드할 데이터 타입.</typeparam>
    public interface IDataLoader<T> where T : IData
    {
        /// <summary>정의 목록을 로드해 반환한다.</summary>
        IReadOnlyList<T> Load();
    }
}
