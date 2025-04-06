using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/State/States/Recover")]
public class EnemyState_Recover : EnemyState
{
    [SerializeField] float duration;
    [SerializeField] float velDamp;
    
    public override void Enter(Enemy context)
    {
        base.Enter(context);
        Debug.Log($"Enemy {context.name}: Recover");
        context.Attacking.RecoveryTimer.Restart();
    }

    public override void PhysicsUpdate(Enemy context)
    {
        base.PhysicsUpdate(context);
        context.RB.linearVelocity *= velDamp;
    }
}