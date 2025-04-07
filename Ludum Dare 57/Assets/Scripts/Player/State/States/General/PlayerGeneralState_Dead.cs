using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/General/PlayerGeneralState_Dead")]
public class PlayerGeneralState_Dead : PlayerState
{
    public override void Enter(Player context)
    {
        base.Enter(context);
        PlayerCamera.Instance.DisableRotation();
    }
}