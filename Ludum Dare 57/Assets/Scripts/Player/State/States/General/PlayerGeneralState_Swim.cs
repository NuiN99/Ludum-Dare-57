using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/General/PlayerGeneralState_Swim")]
public class PlayerGeneralState_Swim : PlayerState
{
    public override void LateUpdate(Player context)
    {
        base.LateUpdate(context);
        context.RotateBodyToCamera();
        context.Movement.Move(context.Movement.GetMovementDirection());
    }
}