using System.Collections.Generic;
using NuiN.NExtensions;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsDead { get; set; }
    public bool IsDamageableCrit => false;
    [field: SerializeField] public EntityType Type { get; private set; }
    public bool IsStunned { get; private set; }
    public int CurHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public Vector3 Position => transform.position;

    [SerializeField, InjectComponent] Enemy enemy;
    [SerializeField] int maxHealth;
    [SerializeField] float stunDuration = 0.75f;
    [SerializeField] float dieDuration = 0.75f;
    [SerializeField] List<ParticleSystem> deathParticles;
    [SerializeField] float deathParticlesSize;

    [SerializeField] FMODSoundPlayer deathSound;

    Coroutine _stunRoutine;

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
        
        if (IsDead) return;
        
        float duration = CurHealth <= 0 ? dieDuration : stunDuration;
        
        IsStunned = true;
        this.StopCoroutineSafe(_stunRoutine);
        _stunRoutine = this.DoAfter(duration, () => IsStunned = false);

        if (CurHealth <= 0)
        {
            IsDead = true;
        }
    }

    public void Die()
    {
        ParticleSpawner.SpawnAll(deathParticles, transform.position, Random.rotation, scaleMultiplier: deathParticlesSize);
        deathSound.PlayAtPosition(transform.position);
        Destroy(enemy.gameObject);
    }
}