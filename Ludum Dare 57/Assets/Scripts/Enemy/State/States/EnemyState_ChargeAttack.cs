using NuiN.NExtensions;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/ChargeAttack")]
public class EnemyState_ChargeAttack : EnemyState
{
    [SerializeField] float chargeMoveBackwardsSpeed;
    [SerializeField] float velDamp;
    
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        context.Attacking.AttackChargeTimer.Restart();
    }

    public override void PhysicsUpdate(Enemy context)
    {
        base.PhysicsUpdate(context);
        if (context.Targeting.Target == null) return;

        context.RB.linearVelocity *= velDamp;
        Vector3 targetDir = VectorUtils.Direction(context.transform.position, context.Targeting.Target.Position);
        context.RB.AddForce(-targetDir * chargeMoveBackwardsSpeed, ForceMode.Acceleration);
    }

    public override void Exit(Enemy context)
    {
        base.Exit(context);
        context.Attacking.AttackChargeTimer.CompleteTimer();
    }
}