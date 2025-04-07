using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/Action/PlayerActionState_Dash")]
public class PlayerActionState_Dash : PlayerState
{
    [SerializeField] FMODSoundPlayer dashSound;
    public override void Enter(Player context)
    {
        base.Enter(context);
        context.Movement.Dash();
        dashSound.PlayEvent();
    }
}