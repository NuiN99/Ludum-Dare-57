using System.Collections.Generic;
using NuiN.NExtensions;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField, InjectComponent] Enemy enemy;
    [SerializeField] float attackRange;

    public Timer AttackTimer { get; private set; }
    
    public void RestartAttackTimer(float newDuration) => AttackTimer = new Timer(newDuration);

    public bool IsInAttackRangeOfTarget()
    {
        return Vector3.Distance(transform.position, enemy.Targeting.Target.Position) <= attackRange;
    }
    
    public List<Collider> CurrentHitTargets { get; } = new();
}