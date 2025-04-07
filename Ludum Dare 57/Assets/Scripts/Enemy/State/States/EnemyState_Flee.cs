using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Flee")]
public class EnemyState_Flee : EnemyState
{
    [SerializeField] float fleeSpeed;

    public override void Enter(Enemy context)
    {
        base.Enter(context);
        context.Attacking.FleeTimer.Restart();
    }

    public override void PhysicsUpdate(Enemy context)
    {
        base.PhysicsUpdate(context);
        
        Vector3 dir = -VectorUtils.Direction(context.transform.position, context.Targeting.Target.Position);
        Vector3 targetDir = dir.With(y: Mathf.Abs(dir.y * 0.1f)).normalized;

        context.RB.linearVelocity = targetDir * fleeSpeed;
    }

    public override void FrameUpdate(Enemy context)
    {
        base.FrameUpdate(context);
        context.transform.rotation = Quaternion.LookRotation(context.RB.linearVelocity);
    }
}