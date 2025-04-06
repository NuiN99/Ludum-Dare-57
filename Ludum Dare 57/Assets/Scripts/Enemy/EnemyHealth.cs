using NuiN.NExtensions;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsStunned { get; private set; }
    
    [SerializeField, InjectComponent] Enemy enemy;
    [SerializeField] float stunDuration = 0.75f;

    public Vector3 Position => transform.position;

    public void TakeDamage(int damage, Vector3 direction)
    {
        Debug.Log("took damage!");

        enemy.RB.linearVelocity = Vector3.zero;
        enemy.RB.AddForce(direction * (damage * 5f), ForceMode.Impulse);
        
        IsStunned = true;
        this.DoAfter(stunDuration, () => IsStunned = false);
    }
}