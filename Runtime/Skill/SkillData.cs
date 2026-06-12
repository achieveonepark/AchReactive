using System;

namespace AchReactive
{
    /// <summary>
    /// 스킬 도메인 데이터. EntityData 의 id/stats/reactions 에 더해 스킬 고유 필드를 가진다.
    /// 실행 로직은 별도 클래스가 없다 — reactions 와 등록된 effect 풀로 전부 표현된다.
    /// </summary>
    [Serializable]
    public class SkillData : EntityData
    {
        /// <summary>쿨타임(초). 런타임에서 CooldownStore 와 함께 사용.</summary>
        public float cooldown;

        /// <summary>캐스팅 시간(초).</summary>
        public float castTime;
    }
}
