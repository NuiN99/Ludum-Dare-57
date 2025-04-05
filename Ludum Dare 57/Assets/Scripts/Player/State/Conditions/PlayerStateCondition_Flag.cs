using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/Conditions/Flag")]
public class PlayerStateCondition_Flag : Condition<Player>
{
    public enum FlagType
    {
        HasSpear,
    }

    [SerializeField] FlagType flag;

    protected override bool IsConditionMet(Player context)
    {
        return flag switch
        {
            FlagType.HasSpear => context.SpearThrowing.HasSpear,
            _ => false
        };
    }
}