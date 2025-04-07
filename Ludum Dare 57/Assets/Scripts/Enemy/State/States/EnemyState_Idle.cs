using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Idle")]
public class EnemyState_Idle : EnemyState
{
    public override void Enter(Enemy context)
    {
        base.Enter(context);
    }

    public override void Exit(Enemy context)
    {
        base.Exit(context);
        context.RB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public override void FrameUpdate(Enemy context)
    {
        base.FrameUpdate(context);
        context.Targeting.TryDetectTarget();
    }
}