using NuiN.NExtensions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool CanDash => _dashCooldownTimer.IsComplete;
    
    [SerializeField] Player player;
    [SerializeField] Rigidbody rb;

    Timer _dashCooldownTimer;

    void Awake()
    {
        _dashCooldownTimer = new Timer(player.Stats.DashCooldown, true);
    }

    public void Move(Vector3 direction, float speedMult = 1f)
    {
        rb.AddForce(direction * (player.Stats.MoveSpeed * speedMult));
    }

    public void Dash()
    {
        Vector3 dir = GetMovementDirection();
        if (dir == Vector3.zero) dir = PlayerCamera.Instance.Forward;
        rb.linearVelocity = dir * player.Stats.DashForce;
        _dashCooldownTimer.Restart();
    }
    
    public Vector3 GetMovementDirection()
    {
        Transform camTransform = PlayerCamera.Instance.CinemachineCam.transform;

        Vector2 input = InputManager.MoveInput;
        Vector3 moveVector = ((camTransform.forward * input.y) + (camTransform.right * input.x));
        //if(InputManager.Controls.Actions.Ascend.IsPressed()) moveVector.y = 1f;
        
        return moveVector.normalized;
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * player.Stats.Gravity, ForceMode.Acceleration);
        rb.linearVelocity *= player.Stats.VelocityDamper;
    }
}