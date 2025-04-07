using UnityEngine;

public class PilotableSubmarine : MonoBehaviour, IInteractable
{
    [SerializeField] Transform pilotTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider col;

    [Header("Movement")] 
    [SerializeField] float moveSpeed;
    
    [SerializeField] float turnTorque = 50f;
    [SerializeField] float maxAngularSpeed = 1f;
    
    bool _isActive;

    void Start()
    {
        col.enabled = false;
        rb.isKinematic = true;
    }

    public void SetRepaired()
    {
        col.enabled = true;
    }

    public void Interact(Player player)
    {
        if (_isActive) return;
        
        PlayerCamera.Instance.SetTrackingTarget(pilotTransform);
        PlayerCamera.Instance.SetLookRotation(Quaternion.LookRotation(pilotTransform.forward));
        PlayerCamera.Instance.DisableRotation();
        PlayerCamera.Instance.EnableRotateToFollowTarget();

        rb.isKinematic = false;
        _isActive = true;
        player.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!_isActive)
        {
            return;
        }
        
        Move();
        Rotate();
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
}