using System.Collections.Generic;
using UnityEngine;

namespace AchReactive
{
    /// <summary>
    /// 패키지가 기본 제공하는 effect 풀. [Effect] 어트리뷰트로 자동 등록되며,
    /// 모든 도메인(스킬/몬스터/아이템 등)이 공유한다. 게임이 같은 이름으로 다시 등록하면 덮어쓴다.
    ///
    /// 대상 규칙: ctx.Targets 가 있으면 전부에, 없으면 ctx.Target 에 적용한다.
    /// 스탯 관례는 <see cref="StatKeys"/> 참고.
    /// </summary>
    public static class BuiltinEffects
    {
        /// <summary>
        /// 피해를 준다. params: power(계수, 기본 1), flat(고정 가산, 기본 0).
        /// 데미지 = max(0, atk*power + flat - def). hp(GetBase)에서 차감.
        /// </summary>
        [Effect("damage")]
        public static void Damage(ReactionContext ctx)
        {
            float power = ctx.Params.Get("power", 1f);
            float flat = ctx.Params.Get("flat", 0f);
            float atk = ctx.Source != null ? ctx.Source.Stats.Get(StatKeys.Atk) : 0f;

            foreach (IEntity t in Targets(ctx))
            {
                float def = t.Stats.Get(StatKeys.Def);
                float dmg = Mathf.Max(0f, atk * power + flat - def);
                t.Stats.SetBase(StatKeys.Hp, t.Stats.GetBase(StatKeys.Hp) - dmg);
            }
        }

        /// <summary>
        /// 회복한다. params: power(계수, 기본 0), flat(고정 회복, 기본 0).
        /// 회복량 = atk*power + flat. hp_max 가 설정돼 있으면 초과하지 않는다.
        /// </summary>
        [Effect("heal")]
        public static void Heal(ReactionContext ctx)
        {
            float power = ctx.Params.Get("power", 0f);
            float flat = ctx.Params.Get("flat", 0f);
            float atk = ctx.Source != null ? ctx.Source.Stats.Get(StatKeys.Atk) : 0f;
            float amount = atk * power + flat;

            foreach (IEntity t in Targets(ctx))
            {
                float hp = t.Stats.GetBase(StatKeys.Hp) + amount;
                float max = t.Stats.Get(StatKeys.HpMax);
                if (max > 0f && hp > max)
                    hp = max;
                t.Stats.SetBase(StatKeys.Hp, hp);
            }
        }

        /// <summary>스탯 기본값을 설정한다. params: stat(키), value.</summary>
        [Effect("set_stat")]
        public static void SetStat(ReactionContext ctx)
        {
            string stat = ctx.Params.Get<string>("stat");
            if (string.IsNullOrEmpty(stat))
                return;

            float value = ctx.Params.Get("value", 0f);
            foreach (IEntity t in Targets(ctx))
                t.Stats.SetBase(stat, value);
        }

        /// <summary>스탯 기본값을 증감한다. params: stat(키), delta.</summary>
        [Effect("add_stat")]
        public static void AddStat(ReactionContext ctx)
        {
            string stat = ctx.Params.Get<string>("stat");
            if (string.IsNullOrEmpty(stat))
                return;

            float delta = ctx.Params.Get("delta", 0f);
            foreach (IEntity t in Targets(ctx))
                t.Stats.SetBase(stat, t.Stats.GetBase(stat) + delta);
        }

        /// <summary>
        /// 스탯 수정자를 더한다(버프 효과의 기본형). params: stat, modifier(flat/percent_add/percent_mul),
        /// value, source(제거용 토큰 문자열, 선택).
        /// </summary>
        [Effect("add_modifier")]
        public static void AddModifier(ReactionContext ctx)
        {
            string stat = ctx.Params.Get<string>("stat");
            if (string.IsNullOrEmpty(stat))
                return;

            string modifier = ctx.Params.Get("modifier", StatModifierType.Flat);
            float value = ctx.Params.Get("value", 0f);
            object source = ctx.Params.Get<string>("source") ?? (object)"add_modifier";

            foreach (IEntity t in Targets(ctx))
                t.Stats.AddModifier(stat, modifier, value, source);
        }

        /// <summary>특정 source 토큰의 수정자를 제거한다. params: source.</summary>
        [Effect("remove_modifier")]
        public static void RemoveModifier(ReactionContext ctx)
        {
            string source = ctx.Params.Get<string>("source");
            if (string.IsNullOrEmpty(source))
                return;

            foreach (IEntity t in Targets(ctx))
                t.Stats.RemoveSource(source);
        }

        /// <summary>World 를 통해 엔티티를 스폰한다. params: entityId, atSource(기본 true).</summary>
        [Effect("spawn")]
        public static void Spawn(ReactionContext ctx)
        {
            if (ctx.World == null)
                return;

            string entityId = ctx.Params.Get<string>("entityId");
            if (string.IsNullOrEmpty(entityId))
                return;

            Vector3 at = ctx.Source != null ? ctx.Source.Position : Vector3.zero;
            ctx.World.Spawn(entityId, at);
        }

        /// <summary>디버그 로그를 남긴다. params: message.</summary>
        [Effect("log")]
        public static void Log(ReactionContext ctx)
        {
            string message = ctx.Params.Get("message", string.Empty);
            Debug.Log($"[AchReactive] {message}");
        }

        /// <summary>이 리액션의 유효 대상(Targets 우선, 없으면 Target)을 열거한다.</summary>
        private static IEnumerable<IEntity> Targets(ReactionContext ctx)
        {
            if (ctx.Targets != null && ctx.Targets.Count > 0)
            {
                foreach (IEntity t in ctx.Targets)
                    if (t != null)
                        yield return t;
            }
            else if (ctx.Target != null)
            {
                yield return ctx.Target;
            }
        }
    }
}
