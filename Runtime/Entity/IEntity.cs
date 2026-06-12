using UnityEngine;

namespace AchReactive
{
    /// <summary>
    /// 리액션의 주체/대상이 되는 게임 객체의 통합 경계. 사용자 게임이 구현한다.
    /// (기존 IBattleUnit 의 도메인 무관 일반화 버전.)
    /// </summary>
    public interface IEntity
    {
        /// <summary>이 엔티티가 참조하는 데이터 정의의 id (선택적).</summary>
        string Id { get; }

        /// <summary>월드 좌표. 일부 effect(스폰/VFX 등)가 사용한다.</summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// 수치 스탯 시트(hp, atk, def 등). 패키지가 제공하는 <see cref="StatSheet"/> 를
        /// 그대로 노출하면 된다. damage/heal 등 기본 effect 가 이 시트를 읽고 쓴다.
        /// </summary>
        StatSheet Stats { get; }

        /// <summary>이 엔티티가 살아있는지(예: hp &gt; 0). 기본 condition 'alive' 가 사용한다.</summary>
        bool IsAlive { get; }
    }
}
