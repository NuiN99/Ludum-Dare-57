using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/States/Action/PlayerActionState_Poke")]
public class PlayerActionState_Poke : PlayerState
{
    public override void Enter(Player context)
    {
        base.Enter(context);
        context.SpearHandling.Poke();
    }
}