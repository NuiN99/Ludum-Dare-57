using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/Action/PlayerActionState_Radar")]
public class PlayerActionState_Radar : PlayerState
{
    public override void Enter(Player context)
    {
        base.Enter(context);
        context.Radar.OpenRadar();
    }

    public override void Exit(Player context)
    {
        base.Exit(context);
        context.Radar.CloseRadar();
    }
}