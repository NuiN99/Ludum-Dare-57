using NuiN.NExtensions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool CanDash => _dashCooldownTimer.IsComplete;
    
    [SerializeField] Player player;
    [SerializeField] FMODSoundPlayer dashSound;

    Timer _dashCooldownTimer;

    public bool IsDashOnCooldown => !_dashCooldownTimer.IsComplete;

    void Awake()
    {
        _dashCooldownTimer = new Timer(player.Stats.DashCooldown, true);
    }

    public void Move(Vector3 direction, float speedMult = 1f)
    {
        player.RB.AddForce(direction * (player.Stats.MoveSpeed * speedMult));
    }

    public void TryDash()
    {
        if (_dashCooldownTimer.IsComplete && !player.Health.IsDead && !player.Radar.IsOpen)
        {
            Vector3 dir = GetMovementDirection();
            if (dir == Vector3.zero) dir = PlayerCamera.Instance.Forward;
            player.RB.linearVelocity = dir * player.Stats.DashForce;
            _dashCooldownTimer.Restart();
            dashSound.PlayEvent();
        }
    }
    
    public Vector3 GetMovementDirection()
    {
        Transform camTransform = PlayerCamera.Instance.CinemachineCam.transform;

        Vector2 input = InputManager.MoveInput;
        Vector3 moveDir = ((camTransform.forward * input.y) + (camTransform.right * input.x)).normalized;
        
        return moveDir;
    }

    void FixedUpdate()
    {
        player.RB.AddForce(Vector3.down * player.Stats.Gravity, ForceMode.Acceleration);
        player.RB.linearVelocity *= player.Stats.VelocityDamper;
    }
}