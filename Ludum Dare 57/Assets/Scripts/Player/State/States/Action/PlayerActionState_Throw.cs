using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/Action/PlayerActionState_Throw")]
public class PlayerActionState_Throw : PlayerState
{
    public override void Enter(Player context)
    {
        base.Enter(context);

        Vector3 centerPos = context.Head.position + PlayerCamera.Instance.Forward * 1000f;
        Vector3 throwDir = VectorUtils.Direction(context.SpearHandling.SpearPosition, centerPos);
        
        context.SpearHandling.Throw(throwDir * context.Stats.SpearThrowForce, context.Stats.SpearThrowDamage);
    }
}