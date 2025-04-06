using System.Collections.Generic;
using NuiN.NExtensions;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [field: SerializeField] public EntityType Type { get; private set; }
    public bool IsStunned { get; private set; }
    public int CurHealth { get; private set; }
    public Vector3 Position => transform.position;

    [SerializeField, InjectComponent] Enemy enemy;
    [SerializeField] int maxHealth;
    [SerializeField] float stunDuration = 0.75f;
    [SerializeField] List<ParticleSystem> deathParticles;
    [SerializeField] float deathParticlesSize;

    void Start()
    {
        CurHealth = maxHealth;
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        Debug.Log("took damage!");

        CurHealth -= damage;

        enemy.RB.linearVelocity = Vector3.zero;
        enemy.RB.AddForce(direction * (damage * 5f), ForceMode.Impulse);
        
        IsStunned = true;
        this.DoAfter(stunDuration, () => IsStunned = false);
    }

    public void Die()
    {
        ParticleSpawner.SpawnAll(deathParticles, transform.position, Random.rotation, scaleMultiplier: deathParticlesSize);
        Destroy(enemy.gameObject);
    }
}