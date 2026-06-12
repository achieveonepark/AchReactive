namespace AchReactive
{
    /// <summary>
    /// 모든 데이터 정의의 공통 계약. id 로 조회되는 모든 도메인(스킬/몬스터/아이템/맵/퀘스트)이 구현한다.
    /// </summary>
    public interface IData
    {
        /// <summary>데이터베이스 내에서 고유한 식별자.</summary>
        string Id { get; }
    }
}
