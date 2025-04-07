using UnityEngine;

public class EnemyWeakPoint : MonoBehaviour, IDamageable
{
    [SerializeField] Enemy enemy;
    [SerializeField] EntityType type;

    [SerializeField] FMODSoundPlayer critSound;
    
    public bool IsDead { get; set; }
    public bool IsDamageableCrit => true;
    public EntityType Type => type;
    public Vector3 Position => transform.position;
    public void TakeDamage(int damage, Vector3 direction)
    {
        if (enemy.Health.IsDead) return;
        
        enemy.Health.TakeDamage(damage * 3, direction);
        IsDead = enemy.Health.CurHealth <= 0;
        critSound.PlayAtPosition(transform.position);
    }
}
