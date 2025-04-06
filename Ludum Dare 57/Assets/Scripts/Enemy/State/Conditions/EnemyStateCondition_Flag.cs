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
            _ => false
        };
    }
}
