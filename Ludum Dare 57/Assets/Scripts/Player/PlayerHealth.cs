using NuiN.NExtensions;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public bool IsDamageableCrit => false;
    public EntityType Type => EntityType.Player;
    public Vector3 Position => transform.position;
    public float CurrentHealth { get; private set; }
    public bool IsDead { get; set; }
    
    [SerializeField] int maxHealth;
    [SerializeField] Player player;

    [SerializeField] CameraShakeOptions damageShakeMax;

    [SerializeField] ParticleSystem damagedParticles;
    [SerializeField] float damagedParticlesSizeMult;

    Timer _invincibilityTimer;
    Timer _regenTimer;

    void Start()
    {
        CurrentHealth = player.Stats.MaxHealth;
        _invincibilityTimer = new Timer(player.Stats.HitInvicibilityDuration, true);
        _regenTimer = new Timer(player.Stats.RechargeHealthDuration);
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        if (IsDead || !_invincibilityTimer.IsComplete)
        {
            return;
        }
        
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, player.Stats.MaxHealth);
        
        float healthLerp = CurrentHealth / player.Stats.MaxHealth;
        PlayerCamera.Instance.Shake(damageShakeMax.WithMult(1 - healthLerp));
        
        Debug.Log($"Player took {damage} damage");
        
        _regenTimer.Restart();
        _invincibilityTimer.Restart();

        ParticleSpawner.Spawn(damagedParticles, transform.position, Random.rotation, scaleMultiplier: damagedParticlesSizeMult);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        IsDead = true;
        GameEvents.InvokePlayerDied();
    }

    void Update()
    {
        if (IsDead || CurrentHealth >= player.Stats.MaxHealth) return;
        
        if (_regenTimer.IsComplete)
        {
            _regenTimer.Restart();
            CurrentHealth++;
            Debug.Log($"Player health ({CurrentHealth}) regened");
        }
    }
}