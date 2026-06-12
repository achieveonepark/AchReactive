namespace AchReactive
{
    /// <summary>
    /// 기본 effect/condition 이 사용하는 관례적 스탯 키. 게임이 다른 키 체계를 쓰면
    /// 직접 effect 를 등록해 덮어쓸 수 있다(이 키들은 어디까지나 기본값).
    /// </summary>
    public static class StatKeys
    {
        /// <summary>현재 체력(리소스). GetBase/SetBase 로 직접 증감한다.</summary>
        public const string Hp = "hp";

        /// <summary>최대 체력. 비율 조건(hp_below 등)의 분모.</summary>
        public const string HpMax = "hp_max";

        /// <summary>공격력(파생 스탯, 수정자 적용).</summary>
        public const string Atk = "atk";

        /// <summary>방어력(파생 스탯, 수정자 적용).</summary>
        public const string Def = "def";
    }
}
