using Modules.Rendering.Outline;
using NuiN.NExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PilotableSubmarine : MonoBehaviour, IInteractable
{
    [SerializeField] Transform pilotTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider col;

    [Header("Movement")] 
    [SerializeField] float moveSpeed;
    
    [SerializeField] float turnTorque = 50f;
    [SerializeField] float maxAngularSpeed = 1f;

    [SerializeField] float dashSpeed;

    [SerializeField] float dashCooldown;
    [SerializeField] PlayerRadar radar;
    [SerializeField] OutlineComponent cockPitOutline;
    [SerializeField] Transform shootPos;
    [SerializeField] Projectile torpedoPrefab;
    [SerializeField] ParticleSystem torpedoExplosion;
    [SerializeField] float shootCooldown;
    [SerializeField] float shootForce;
    [SerializeField] float torpedoExplodeRadius;
    [SerializeField] int torpedoDamage = 5;
    
    [SerializeField] FMODSoundPlayer dashSound;
    [SerializeField] FMODSoundPlayer torpedoShootSound;
    [SerializeField] FMODSoundPlayer torpedoExplodeSound;
    
    [SerializeField] FMODSoundPlayer idleSound;
    [SerializeField] FMODSoundPlayer repairedSound;

    [SerializeField] FMODSoundPlayer collisionSound;
    
    bool _isActive;
    
    Timer _dashCooldownTimer;
    Timer _shootCooldownTimer;

    void Awake()
    {
        _dashCooldownTimer = new Timer(dashCooldown, true);
        _shootCooldownTimer = new Timer(shootCooldown, true);
        
        col.enabled = false;
        rb.isKinematic = true;
    }
    
    void OnDisable()
    {
        InputManager.Controls.Actions.Dash.performed -= Dash_Callback;
        InputManager.Controls.Actions.Attack.performed -= Shoot_Callback;
    }
    

    public void SetRepaired()
    {
        idleSound.PlayEventAttached(pilotTransform);
        repairedSound.PlayEventAttached(pilotTransform);
        col.enabled = true;
    }

    public void Interact(Player player)
    {
        if (_isActive) return;
        
        InputManager.Controls.Actions.Dash.performed += Dash_Callback;
        InputManager.Controls.Actions.Attack.performed += Shoot_Callback;
        
        PlayerCamera.Instance.SetTrackingTarget(pilotTransform);
        PlayerCamera.Instance.SetLookRotation(Quaternion.LookRotation(pilotTransform.forward));
        PlayerCamera.Instance.DisableRotation();
        PlayerCamera.Instance.EnableRotateToFollowTarget();
        
        radar.OpenRadar();
        cockPitOutline.enabled = false;

        rb.isKinematic = false;
        _isActive = true;
        player.gameObject.SetActive(false);
        
        GameEvents.InvokePlayerEnterSubmarine();
    }

    void FixedUpdate()
    {
        if (!_isActive)
        {
            return;
        }
        
        Move();
        Rotate();

        Player.Instance.transform.position = pilotTransform.position;
    }

    void Move()
    {
        Vector3 moveDir = GetMovementDirection();
        if (moveDir == Vector3.zero) return;

        rb.AddForce(moveDir * moveSpeed, ForceMode.Acceleration);
    }

    
    void Rotate()
    {
        Vector2 rotateInput = InputManager.RotateInput;
        if (rotateInput == Vector2.zero) return;

        Vector3 yawTorque = transform.up * (rotateInput.x * turnTorque);

        Vector3 camRight = PlayerCamera.Instance.CinemachineCam.transform.right;
        Vector3 pitchAxis = Vector3.ProjectOnPlane(camRight, transform.up).normalized;
        Vector3 pitchTorque = pitchAxis * (-rotateInput.y * turnTorque);

        Vector3 totalTorque = yawTorque + pitchTorque;

        if (rb.angularVelocity.magnitude < maxAngularSpeed)
        {
            rb.angularVelocity += totalTorque;
        }
    }

    Vector3 GetMovementDirection()
    {
        Transform camTransform = PlayerCamera.Instance.CinemachineCam.transform;

        Vector2 input = InputManager.MoveInput;
        Vector3 moveDir = ((camTransform.forward * input.y) + (camTransform.right * input.x)).normalized;
        
        return moveDir;
    }

    void Dash_Callback(InputAction.CallbackContext ctx)
    {
        if (!_dashCooldownTimer.IsComplete)
        {
            return;
        }

        float inputDir = InputManager.MoveInput.y;
        Vector3 dir = PlayerCamera.Instance.Forward * inputDir;
        rb.linearVelocity = dir * dashSpeed;

        dashSound.PlayEvent();
        
        _dashCooldownTimer.Restart();
    }

    void Shoot_Callback(InputAction.CallbackContext ctx)
    {
        if (!_shootCooldownTimer.IsComplete)
        {
            return;
        }

        torpedoShootSound.PlayEvent();

        Vector3 forceVector = shootPos.forward * shootForce;
        Quaternion rot = Quaternion.LookRotation(forceVector);
        Projectile torpedo = Instantiate(torpedoPrefab, shootPos.position, rot);
        torpedo.Launch(forceVector, torpedoDamage, OnHit_Callback);

        torpedo.DoAfter(15f, () => Destroy(torpedo.gameObject));
        
        _shootCooldownTimer.Restart();
    }

    void OnHit_Callback(Collision collision, Projectile projectile, int damage)
    {
        Vector3 hitPoint = collision.GetContact(0).point;
        ParticleSpawner.Spawn(torpedoExplosion, hitPoint, Random.rotation);
        
        torpedoExplodeSound.PlayAtPosition(hitPoint);

        Collider[] hits = Physics.OverlapSphere(hitPoint, torpedoExplodeRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable damageable) && damageable.Type != EntityType.Player)
            {
                damageable.TakeDamage(damage, Vector3.zero);
            }
        }

        ParticleSystem particles = projectile.GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            particles.transform.SetParent(null);
        }
        
        Destroy(projectile.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        collisionSound.PlayEvent();
    }
}