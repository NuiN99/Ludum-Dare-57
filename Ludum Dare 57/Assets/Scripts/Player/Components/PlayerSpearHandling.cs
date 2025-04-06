using NuiN.NExtensions;
using UnityEngine;

public class PlayerSpearHandling : MonoBehaviour
{
    public bool HasSpear { get; private set; }
    public bool IsPoking => !_spearPokeDurationTimer.IsComplete;
    
    [SerializeField] Player player;
    [SerializeField] Projectile spear;
    [SerializeField] Transform spearParent;

    Timer _spearPokeDurationTimer;

    void Awake()
    {
        _spearPokeDurationTimer = new Timer(player.Stats.SpearPokeDuration, true);
    }

    void Start()
    {
        Retrieve();
    }

    public void Throw(Vector3 force, int damage)
    {
        spear.TogglePhysics(true);
        spear.ToggleRotation(true);
        spear.transform.SetParent(null);
        spear.Launch(force, damage, OnHit_Callback);
        
        HasSpear = false;
    }

    public void Retrieve()
    {
        spear.transform.SetParent(spearParent);
        spear.transform.localPosition = Vector3.zero;
        spear.transform.localRotation = Quaternion.identity;
        spear.TogglePhysics(false);
        spear.ToggleRotation(false);
        
        HasSpear = true;
    }

    public void Poke()
    {
        _spearPokeDurationTimer.Restart();
        
        RaycastHit[] hits = Physics.SphereCastAll(player.Head.position, player.Stats.SpearPokeRadius, PlayerCamera.Instance.Forward, player.Stats.SpearPokeRange);
        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.TryGetComponent(out IDamageable damageable)) continue;
            
            damageable?.TakeDamage(player.Stats.SpearPokeDamage, PlayerCamera.Instance.Forward);
        }
    }

    void OnHit_Callback(Collision collision, int damage)
    {
        spear.ToggleRotation(false);
        spear.RB.linearVelocity *= 0.25f;
        
        if (collision.collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage, PlayerCamera.Instance.Forward);
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