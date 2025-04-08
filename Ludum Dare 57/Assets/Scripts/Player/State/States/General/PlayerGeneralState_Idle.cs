using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/General/PlayerGeneralState_Idle")]
public class PlayerGeneralState_Idle : PlayerState
{
    public override void LateFrameUpdate(Player context)
    {
        base.LateFrameUpdate(context);
        context.RotateBodyToCamera();
    }
    
    public override void FrameUpdate(Player context)
    {
        base.FrameUpdate(context);
        if (InputManager.Controls.Actions.Dash.WasPressedThisFrame())
        {
            context.Movement.TryDash();
        }
    }
}