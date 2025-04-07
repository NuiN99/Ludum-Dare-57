using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/Action/PlayerActionState_HoldItem")]
public class PlayerActionState_HoldItem : PlayerState
{
    public override void Enter(Player context)
    {
        base.Enter(context);
        GameEvents.InvokePlayerHoldingPartStateChanged(true);
    }

    public override void Exit(Player context)
    {
        base.Exit(context);
        GameEvents.InvokePlayerHoldingPartStateChanged(false);
        context.Interaction.ReleaseHeldPart();
    }
}