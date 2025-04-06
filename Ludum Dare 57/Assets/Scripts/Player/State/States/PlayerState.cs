using NuiN.NExtensions;
using UnityEngine;

public class PlayerState : State<Player>
{
    public PriorityAnimator.AnimationPlayData AnimationPlayData => animationPlayData;

    [SerializeField] PriorityAnimator.AnimationPlayData animationPlayData;
    
    public override void Enter(Player context)
    {
        context.PriorityAnimator.Play(animationPlayData);
    }

    public override void Exit(Player context)
    {
        if (context.PriorityAnimator.GetCurrentAnimation(animationPlayData.LayerIndex) != animationPlayData.Animation)
        {
            return;
        }
        
        context.PriorityAnimator.ResetLayer(animationPlayData.LayerIndex);
    }
}
