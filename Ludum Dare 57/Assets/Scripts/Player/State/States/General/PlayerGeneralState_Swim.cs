using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/General/PlayerGeneralState_Swim")]
public class PlayerGeneralState_Swim : PlayerState
{
    public override void LateFrameUpdate(Player context)
    {
        base.LateFrameUpdate(context);
        context.RotateBodyToCamera();
    }

    public override void PhysicsUpdate(Player context)
    {
        base.PhysicsUpdate(context);
        context.Movement.Move(context.Movement.GetMovementDirection());
    }
}