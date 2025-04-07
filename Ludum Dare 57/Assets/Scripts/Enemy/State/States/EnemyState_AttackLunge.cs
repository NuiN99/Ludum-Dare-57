using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/AttackLunge")]
public class EnemyState_AttackLunge : EnemyState
{
    [SerializeField] float lungeSpeed = 20f;
    [SerializeField] float lungeDuration = 1f;
    [SerializeField] float damageRadius = 3f;
    [SerializeField] float lungeSpread = 3f;

    [SerializeField] FMODSoundPlayer biteSound;
    
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        
        context.Attacking.RestartAttackTimer(lungeDuration);
        
        Vector3 generalTargetPos = context.Targeting.Target.Position + Random.insideUnitSphere * lungeSpread;
        Vector3 dir = VectorUtils.Direction(context.transform.position, generalTargetPos);
        
        context.transform.rotation = Quaternion.LookRotation(dir);
        
        context.Attacking.SetAttackDir(dir);
        
        context.Attacking.CurrentHitTargets.Clear();
    }

    public override void FrameUpdate(Enemy context)
    {
        base.FrameUpdate(context);
        Collider[] colliders = Physics.OverlapSphere(context.transform.position, damageRadius);

        foreach (Collider col in colliders)
        {
            if(col.transform == context.transform || context.Attacking.CurrentHitTargets.Contains(col)) continue;
            if (col.TryGetComponent(out IDamageable damageable) && context.Targeting.IsValidTarget(damageable))
            {
                context.Attacking.CurrentHitTargets.Add(col);
                damageable.TakeDamage(1, context.transform.forward);
                biteSound.PlayEventAttached(context.transform);
            }
        }

        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, Quaternion.LookRotation(context.RB.linearVelocity), 10f * Time.deltaTime);
    }

    public override void PhysicsUpdate(Enemy context)
    {
        base.PhysicsUpdate(context);

        Vector3 targetDir = VectorUtils.Direction(context.transform.position, context.Targeting.Target.Position);
        context.RB.AddForce((context.Attacking.AttackDir + targetDir).normalized  * lungeSpeed, ForceMode.Acceleration);;
    }

    public override void Exit(Enemy context)
    {
        base.Exit(context);
        context.Attacking.CurrentHitTargets.Clear();
        context.Attacking.AttackTimer.CompleteTimer();
    }

    public override void DrawGizmos(Enemy context)
    {
        base.DrawGizmos(context);

        Gizmos.color = Color.red.WithAlpha(0.1f);
        Gizmos.DrawSphere(context.transform.position, damageRadius);
    }
}