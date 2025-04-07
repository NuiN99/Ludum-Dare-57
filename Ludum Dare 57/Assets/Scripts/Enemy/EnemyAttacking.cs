using System.Collections.Generic;
using NuiN.NExtensions;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField, InjectComponent] Enemy enemy;
    [SerializeField] float attackRange;
    [SerializeField] float recoveryDuration;
    [SerializeField] float attackChargeDuration;
    [SerializeField] float fleeDuration;
    
    public Vector3 AttackDir { get; private set; }
    public List<Collider> CurrentHitTargets { get; } = new();
    public Timer RecoveryTimer { get; private set; }
    public Timer AttackTimer { get; private set; }
    public Timer AttackChargeTimer { get; private set; }
    public Timer FleeTimer { get; private set; }

    void Awake()
    {
        RecoveryTimer = new Timer(recoveryDuration);
        AttackChargeTimer = new Timer(attackChargeDuration);
        FleeTimer = new Timer(fleeDuration);
    }

    public void RestartAttackTimer(float newDuration)
    {
        AttackTimer = new Timer(newDuration);
    }

    public bool IsInAttackRangeOfTarget()
    {
        return Vector3.Distance(transform.position, enemy.Targeting.Target.Position) <= attackRange;
    }

    public void SetAttackDir(Vector3 dir)
    {
        AttackDir = dir;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}