using NuiN.NExtensions;
using UnityEngine;

public class PlayerSpearHandling : MonoBehaviour
{
    public bool HasSpear { get; private set; }
    public bool IsPoking => !_spearPokeDurationTimer.IsComplete;
    public Vector3 SpearPosition => _activeSpear.transform.position;
    
    [SerializeField] Player player;
    [SerializeField] Spear spearPrefab;
    [SerializeField] float explodeForce = 10f; 
    [SerializeField] Vector3 holdOffset;
    [SerializeField] float respawnYThreshold = -50f;
    [SerializeField] ParticleSystem hitEnemyParticles;
    
    [Header("Sounds")]
    [SerializeField] FMODSoundPlayer spearHitFleshSound;
    [SerializeField] FMODSoundPlayer spearCritSound;
    [SerializeField] FMODSoundPlayer spearHitGroundSound;
    [SerializeField] FMODSoundPlayer spearPokeSound;
    [SerializeField] FMODSoundPlayer spearThrowSound;
    [SerializeField] FMODSoundPlayer spearPickupSound;
    
    Timer _spearPokeDurationTimer;
    Spear _activeSpear;
    Spear.Data _activeSpearData;

    void Awake()
    {
        _spearPokeDurationTimer = new Timer(player.Stats.SpearPokeDuration, true);
    }

    void Start()
    {
        _activeSpear = Instantiate(spearPrefab);
        Retrieve();
    }

    public void Throw(Vector3 force, int damage)
    {
        Physics.IgnoreCollision(_activeSpear.Col, player.Col);
        _activeSpear.TogglePhysics(true);
        
        _activeSpear.Col.enabled = true;
        _activeSpear.Col.isTrigger = false;
        
        _activeSpear.ToggleRotation(true);
        _activeSpear.transform.SetParent(null);
        _activeSpear.Launch(force, damage, OnHit_Callback);
        
        HasSpear = false;

        spearThrowSound.PlayEvent();
    }

    public void Retrieve()
    {
        _activeSpear.transform.SetParent(player.Hand);
        _activeSpear.transform.localPosition = holdOffset;
        _activeSpear.transform.localRotation = Quaternion.identity;
        _activeSpear.TogglePhysics(false);
        _activeSpear.ToggleRotation(false);
        _activeSpear.ToggleOutline(false);

        _activeSpear.Col.enabled = false;
        
        HasSpear = true;
        
        spearPickupSound.PlayEvent();
    }

    public void Poke()
    {
        _spearPokeDurationTimer.Restart();

        spearPokeSound.PlayEvent();

        bool hitWall = false;
        Vector3 hitWallPos = Vector3.zero;
        
        RaycastHit[] hits = Physics.SphereCastAll(player.Head.position, player.Stats.SpearPokeRadius, PlayerCamera.Instance.Forward, player.Stats.SpearPokeRange);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform == player.transform) continue;

            if (!hit.collider.TryGetComponent(out IDamageable damageable))
            {
                hitWall = true;
                hitWallPos = hit.point;
                
                continue;
            }

            spearHitFleshSound.PlayAtPosition(hit.point);
            damageable?.TakeDamage(player.Stats.SpearPokeDamage, PlayerCamera.Instance.Forward);
            
            ParticleSpawner.Spawn(hitEnemyParticles, hit.point, Random.rotation);
        }

        if (hitWall)
        {
            spearHitGroundSound.PlayAtPosition(hitWallPos);
        }
    }

    void Update()
    {
        if (_activeSpear != null)
        {
            _activeSpearData = new Spear.Data(_activeSpear.transform.position, _activeSpear.transform.rotation, _activeSpear.RB.linearVelocity);

            if (HasSpear)
            {
                _activeSpear.gameObject.SetActive(player.Interaction.HeldPart == null);
            }

            if (_activeSpear.transform.position.y < respawnYThreshold)
            {
                Retrieve();
            }
        }
        else
        {
            _activeSpear = Instantiate(spearPrefab, _activeSpearData.position, _activeSpearData.rotation);
            _activeSpear.ToggleRotation(false);
            _activeSpear.RB.linearVelocity = _activeSpearData.velocity;
            _activeSpear.RB.AddForce(-_activeSpear.transform.forward * explodeForce, ForceMode.VelocityChange);
        }
    }

    void OnHit_Callback(Collision collision, Projectile _, int damage)
    {
        bool bouncedOffEnemy = false;
        
        Vector3 hitPoint = collision.GetContact(0).point;
        if (collision.collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage, PlayerCamera.Instance.Forward);
            ParticleSpawner.Spawn(hitEnemyParticles, hitPoint, Random.rotation);

            if (damageable.IsDead) _activeSpear.transform.SetParent(collision.transform);
            else
            {
                _activeSpear.RB.linearVelocity = Vector3.zero;
                _activeSpear.RB.AddForce(-_activeSpear.transform.forward * explodeForce * 0.5f, ForceMode.VelocityChange);
                _activeSpear.ToggleOutline(true);
                bouncedOffEnemy = true;
            }
            
            if (damageable.IsDamageableCrit) spearCritSound.PlayAtPosition(hitPoint);
            else spearHitFleshSound.PlayAtPosition(hitPoint);
        }
        else
        {
            _activeSpear.ToggleOutline(true);
            spearHitGroundSound.PlayAtPosition(hitPoint);
        }

        _activeSpear.ToggleRotation(false);
        if (!bouncedOffEnemy)
        {
            _activeSpear.TogglePhysics(false);
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = player.Head.position;
        Vector3 end = player.Head.position + player.Head.forward * player.Stats.SpearPokeRange;
        Gizmos.DrawWireSphere(start, player.Stats.SpearPokeRadius);
        Gizmos.DrawWireSphere(end, player.Stats.SpearPokeRadius);
        Gizmos.DrawLine(start, end);
    }
}