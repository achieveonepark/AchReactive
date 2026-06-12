using System;

namespace AchReactive.Samples
{
    /// <summary>
    /// 두 번째 도메인 예시. 실제로는 Type Designer 가 생성하지만, 여기서는 손으로 작성해
    /// "새 도메인 = 데이터 클래스 1개 + 데이터 파일, 시스템 코드 0" 임을 보여준다.
    /// 몬스터는 스킬과 동일한 effect 풀(add_modifier 등)을 그대로 재사용한다.
    /// </summary>
    [Serializable]
    public class MonsterData : EntityData
    {
        /// <summary>처치 시 주는 경험치(도메인 고유 필드).</summary>
        public int expReward;
    }
}
