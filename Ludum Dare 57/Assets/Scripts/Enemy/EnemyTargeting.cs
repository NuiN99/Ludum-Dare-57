using System.Collections.Generic;
using NuiN.NExtensions;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public IDamageable Target { get; private set; }

    [SerializeField] float searchRadius = 10f;
    [SerializeField] float searchInterval;
    [SerializeField] List<EntityType> validTargets;
    [SerializeField] LayerMask detectMask;

    Timer _searchIntervalTimer;

    void Awake()
    {
        _searchIntervalTimer = new Timer(searchInterval);
    }

    public void TryDetectTarget()
    {
        if (!_searchIntervalTimer.IsComplete) return;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius, detectMask);

        if (colliders.Length <= 0) return;

        IDamageable closest = null;
        float closestDist = Mathf.Infinity;
        foreach (var col in colliders)
        {
            if(col.transform == transform) continue;
            
            float dist = (col.transform.position - transform.position).sqrMagnitude;
            
            if (dist >= closestDist) continue;

            if (!col.TryGetComponent(out IDamageable damageable)) continue;
            
            if(!IsValidTarget(damageable)) continue;
            
            closest = damageable;
            closestDist = dist;
        }
        
        Target = closest;
    }

    public void ForgetTarget()
    {
        Target = null;
    }

    public bool IsValidTarget(IDamageable target)
    {
        return validTargets.Contains(target.Type);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}