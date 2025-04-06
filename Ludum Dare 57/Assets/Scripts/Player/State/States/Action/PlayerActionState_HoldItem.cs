using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/Action/PlayerActionState_HoldItem")]
public class PlayerActionState_HoldItem : PlayerState
{
    public override void Exit(Player context)
    {
        base.Exit(context);
        context.Interaction.ReleaseHeldPart();
    }
}