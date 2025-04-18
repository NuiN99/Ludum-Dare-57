using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/ChaseSimple")]
public class EnemyState_ChaseSimple : EnemyState
{
    [SerializeField] float speed;
    
    public override void Enter(Enemy context)
    {
        base.Enter(context);
    }

    public override void PhysicsUpdate(Enemy context)
    {
        base.PhysicsUpdate(context);

        if (context.Targeting.Target == null) return;
        
        Vector3 targetDir = VectorUtils.Direction(context.transform.position, context.Targeting.Target.Position);
        context.RB.AddForce(targetDir * speed, ForceMode.Acceleration);
    }

    public override void FrameUpdate(Enemy context)
    {
        base.FrameUpdate(context);
        context.transform.rotation = Quaternion.LookRotation(VectorUtils.Direction(context.transform.position, context.Targeting.Target.Position));
    }
}