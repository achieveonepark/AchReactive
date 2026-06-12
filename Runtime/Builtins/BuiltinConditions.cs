using UnityEngine;

namespace AchReactive
{
    /// <summary>
    /// 패키지가 기본 제공하는 condition 풀. [Condition] 으로 자동 등록된다.
    /// 별도 지정이 없으면 Target(없으면 Source)을 평가 대상으로 본다.
    /// </summary>
    public static class BuiltinConditions
    {
        /// <summary>항상 참.</summary>
        [Condition("always")]
        public static bool Always(ReactionContext ctx) => true;

        /// <summary>대상이 살아있는가.</summary>
        [Condition("alive")]
        public static bool Alive(ReactionContext ctx)
        {
            IEntity e = Subject(ctx);
            return e != null && e.IsAlive;
        }

        /// <summary>대상이 죽었는가.</summary>
        [Condition("dead")]
        public static bool Dead(ReactionContext ctx)
        {
            IEntity e = Subject(ctx);
            return e != null && !e.IsAlive;
        }

        /// <summary>확률 통과. params: value(0~1, 기본 1).</summary>
        [Condition("chance")]
        public static bool Chance(ReactionContext ctx)
        {
            float p = ctx.Params.Get("value", 1f);
            return Random.value <= p;
        }

        /// <summary>대상 HP 비율이 value 이하인가. params: value(0~1).</summary>
        [Condition("hp_below")]
        public static bool HpBelow(ReactionContext ctx)
        {
            return HpRatio(Subject(ctx), out float ratio) && ratio <= ctx.Params.Get("value", 0f);
        }

        /// <summary>대상 HP 비율이 value 이상인가. params: value(0~1).</summary>
        [Condition("hp_above")]
        public static bool HpAbove(ReactionContext ctx)
        {
            return HpRatio(Subject(ctx), out float ratio) && ratio >= ctx.Params.Get("value", 0f);
        }

        /// <summary>대상의 특정 스탯이 value 이상인가. params: stat, value.</summary>
        [Condition("stat_above")]
        public static bool StatAbove(ReactionContext ctx)
        {
            IEntity e = Subject(ctx);
            string stat = ctx.Params.Get<string>("stat");
            if (e == null || string.IsNullOrEmpty(stat))
                return false;
            return e.Stats.Get(stat) >= ctx.Params.Get("value", 0f);
        }

        /// <summary>대상의 특정 스탯이 value 이하인가. params: stat, value.</summary>
        [Condition("stat_below")]
        public static bool StatBelow(ReactionContext ctx)
        {
            IEntity e = Subject(ctx);
            string stat = ctx.Params.Get<string>("stat");
            if (e == null || string.IsNullOrEmpty(stat))
                return false;
            return e.Stats.Get(stat) <= ctx.Params.Get("value", 0f);
        }

        private static IEntity Subject(ReactionContext ctx) => ctx.Target ?? ctx.Source;

        private static bool HpRatio(IEntity e, out float ratio)
        {
            ratio = 0f;
            if (e == null)
                return false;
            float max = e.Stats.Get(StatKeys.HpMax);
            if (max <= 0f)
                return false;
            ratio = e.Stats.GetBase(StatKeys.Hp) / max;
            return true;
        }
    }
}
