namespace AchReactive
{
    /// <summary>스탯 수정자 연산 타입 상수.</summary>
    public static class StatModifierType
    {
        /// <summary>고정값 가산. (base + flat) 단계.</summary>
        public const string Flat = "flat";

        /// <summary>가산 퍼센트. 합산되어 (1 + Σ) 로 곱해진다.</summary>
        public const string PercentAdd = "percent_add";

        /// <summary>승산 퍼센트. 각각 (1 + v) 로 누적 곱해진다.</summary>
        public const string PercentMul = "percent_mul";

        public static readonly string[] Supported = { Flat, PercentAdd, PercentMul };
    }
}
