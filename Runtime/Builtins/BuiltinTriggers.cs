namespace AchReactive
{
    /// <summary>
    /// 패키지가 기본 제공하는 트리거 가드 풀. [Trigger] 로 자동 등록된다.
    /// 가드가 없는 트리거(on_use, on_hit, on_equip 등)는 채널 이름만으로 발동하므로
    /// 여기 등록할 필요가 없다. 임계값 비교가 필요한 트리거만 가드를 둔다.
    /// </summary>
    public static class BuiltinTriggers
    {
        /// <summary>
        /// Source 의 HP 비율이 threshold 이하일 때만 발동. triggerParams: threshold(0~1).
        /// 예: 몬스터가 피격(on_hp_below 채널로 Run)될 때 30% 이하면 광폭화.
        /// </summary>
        [Trigger("on_hp_below")]
        public static bool OnHpBelow(ReactionContext ctx, ParamBag triggerParams)
        {
            IEntity e = ctx.Source;
            if (e == null)
                return false;

            float max = e.Stats.Get(StatKeys.HpMax);
            if (max <= 0f)
                return false;

            float ratio = e.Stats.GetBase(StatKeys.Hp) / max;
            return ratio <= triggerParams.Get("threshold", 0f);
        }
    }
}
