using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/Conditions/Flag")]
public class EnemyStateCondition_Flag : Condition<Enemy>
{
    enum FlagType
    {
        IsStunned,
        HasTarget,
        IsAttacking,
        IsInAttackRangeOfTarget,
        IsRecovering,
        IsChargingAttack,
        IsDead,
        IsFleeing,
        HealthIsBelowHalf,
    }

    [SerializeField] FlagType flag;

    protected override bool IsConditionMet(Enemy context)
    {
        return flag switch
        {
            FlagType.IsStunned => context.Health.IsStunned,
            FlagType.HasTarget => context.Targeting.Target != null,
            FlagType.IsAttacking => !context.Attacking.AttackTimer.IsComplete,
            FlagType.IsInAttackRangeOfTarget => context.Attacking.IsInAttackRangeOfTarget(),
            FlagType.IsRecovering => !context.Attacking.RecoveryTimer.IsComplete,
            FlagType.IsChargingAttack => !context.Attacking.AttackChargeTimer.IsComplete,
            FlagType.IsDead => context.Health.CurHealth <= 0,
            FlagType.IsFleeing => !context.Attacking.FleeTimer.IsComplete,
            FlagType.HealthIsBelowHalf => context.Health.CurHealth <= context.Health.MaxHealth / 2,
            _ => false
        };
    }
}