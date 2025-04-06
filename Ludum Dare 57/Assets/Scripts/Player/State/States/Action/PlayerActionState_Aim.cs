using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/Action/PlayerActionState_Aim")]
public class PlayerActionState_Aim : PlayerState
{
    public override void Enter(Player context)
    {
        base.Enter(context);
        GameEvents.InvokeAimStateChanged(true);
    }

    public override void Exit(Player context)
    {
        base.Exit(context);
        GameEvents.InvokeAimStateChanged(false);
    }
}