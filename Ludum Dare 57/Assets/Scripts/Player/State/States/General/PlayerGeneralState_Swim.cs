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

        Vector3 moveDir = context.Movement.GetMovementDirection();
        context.Movement.Move(moveDir);
    }
}