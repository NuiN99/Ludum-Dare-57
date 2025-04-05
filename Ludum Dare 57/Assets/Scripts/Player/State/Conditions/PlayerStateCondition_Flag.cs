using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/Conditions/Flag")]
public class PlayerStateCondition_Flag : Condition<Player>
{
    public enum FlagType
    {
        HasSpear,
        CanDash,
        IsDead,
        IsCheckingRadar,
    }

    [SerializeField] FlagType flag;

    protected override bool IsConditionMet(Player context)
    {
        return flag switch
        {
            FlagType.HasSpear => context.SpearThrowing.HasSpear,
            FlagType.CanDash => context.Movement.CanDash,
            FlagType.IsDead => context.IsDead,
            FlagType.IsCheckingRadar => context.IsCheckingRadar,
            _ => false
        };
    }
}