using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/General/PlayerGeneralState_Idle")]
public class PlayerGeneralState_Idle : PlayerState
{
    public override void LateUpdate(Player context)
    {
        base.LateUpdate(context);
        context.RotateBodyToCamera();
    }
}