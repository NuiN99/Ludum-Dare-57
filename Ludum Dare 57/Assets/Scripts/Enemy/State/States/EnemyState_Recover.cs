using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Recover")]
public class EnemyState_Recover : EnemyState
{
    [SerializeField] float duration;
    [SerializeField] float velDamp;
    
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        context.Attacking.RecoveryTimer.Restart();
    }

    public override void PhysicsUpdate(Enemy context)
    {
        base.PhysicsUpdate(context);
        context.RB.linearVelocity *= velDamp;
    }

    public override void FrameUpdate(Enemy context)
    {
        base.FrameUpdate(context);
        Vector3 dir = VectorUtils.Direction(context.transform.position, context.Targeting.Target.Position);
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, Quaternion.LookRotation(dir), 2f * Time.deltaTime);
    }
}