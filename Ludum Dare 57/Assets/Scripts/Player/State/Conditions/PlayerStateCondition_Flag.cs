using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/Conditions/Flag")]
public class PlayerStateCondition_Flag : Condition<Player>
{
    public enum FlagType
    {
        
    }

    [SerializeField] FlagType flag;

    protected override bool IsConditionMet(Player context)
    {
        return flag switch
        {
            _ => false
        };
    }
}