using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/Conditions/Flag")]
public class PlayerStateCondition_Flag : Condition<Player>
{
    enum FlagType
    {
        HasSpear,
        CanDash,
        IsDead,
        IsCheckingRadar,
        IsPoking,
        IsHoldingPart,
    }

    [SerializeField] FlagType flag;

    protected override bool IsConditionMet(Player context)
    {
        return flag switch
        {
            FlagType.HasSpear => context.SpearHandling.HasSpear,
            FlagType.CanDash => context.Movement.CanDash,
            FlagType.IsDead => context.Health.IsDead,
            FlagType.IsCheckingRadar => context.Radar.IsOpen,
            FlagType.IsPoking => context.SpearHandling.IsPoking,
            FlagType.IsHoldingPart => context.Interaction.HeldPart != null,
            _ => false
        };
    }
}