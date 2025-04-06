using NuiN.NExtensions;
using UnityEngine;

public class PlayerSpearHandling : MonoBehaviour
{
    public bool HasSpear { get; private set; }
    public bool IsPoking => !_spearPokeDurationTimer.IsComplete;
    public Vector3 SpearPosition => spear.transform.position;
    
    [SerializeField] Player player;
    [SerializeField] Projectile spear;
    [SerializeField] Transform spearParent;

    Timer _spearPokeDurationTimer;

    Transform _hitTransform;
    Vector3 _onHitPositionOffset;
    Vector3 _onHitEulerAnglesOffset;
    Vector3 _initialHoldOffset;

    void Awake()
    {
        _spearPokeDurationTimer = new Timer(player.Stats.SpearPokeDuration, true);
    }

    void Start()
    {
        _initialHoldOffset = spear.transform.localPosition;
        Physics.IgnoreCollision(spear.Col, player.Col);
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
        _hitTransform = null;
        
        spear.transform.SetParent(spearParent);
        spear.transform.localPosition = _initialHoldOffset;
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
            if (hit.transform == player.transform || !hit.collider.TryGetComponent(out IDamageable damageable)) continue;
            
            damageable?.TakeDamage(player.Stats.SpearPokeDamage, PlayerCamera.Instance.Forward);
        }
    }

    void Update()
    {
        if (_hitTransform == null)
        {
            if (HasSpear == false && spear.RB.isKinematic)
            {
                spear.TogglePhysics(true);

                spear.RB.AddForce(-spear.transform.forward * 5f, ForceMode.Impulse);
            }
            return;
        }

        Vector3 targetPos = _hitTransform.TransformPoint(-_onHitPositionOffset);

        spear.transform.eulerAngles = _hitTransform.eulerAngles + _onHitEulerAnglesOffset;
        spear.transform.position = targetPos;
    }

    void OnHit_Callback(Collision collision, int damage)
    {
        if (collision.collider.TryGetComponent(out IDamageable damageable))
        {
            _onHitPositionOffset = damageable.Position - spear.RB.position;
            _onHitEulerAnglesOffset = collision.transform.eulerAngles - spear.RB.rotation.eulerAngles;
            _hitTransform = collision.transform;
            
            damageable.TakeDamage(damage, PlayerCamera.Instance.Forward);
        }
        
        spear.ToggleRotation(false);
        spear.TogglePhysics(false);
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