using NuiN.NExtensions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float damper;
    [SerializeField] float gravity;
    
    public void Move(Vector3 direction, float speedMult = 1f)
    {
        rb.AddForce(direction * (moveSpeed * speedMult));
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
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        rb.linearVelocity *= damper;
    }
}