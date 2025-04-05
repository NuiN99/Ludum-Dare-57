using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/Action/PlayerActionState_Throw")]
public class PlayerActionState_Throw : PlayerState
{
    public override void Enter(Player context)
    {
        base.Enter(context);
        context.SpearThrowing.Throw(PlayerCamera.Instance.Forward * context.Stats.SpearThrowForce, context.Stats.SpearThrowDamage);
    }
}