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
        Vector3 moveVector = ((camTransform.forward * input.y) + (camTransform.right * input.x));
        //if(InputManager.Controls.Actions.Ascend.IsPressed()) moveVector.y = 1f;
        
        return moveVector.normalized;
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        rb.linearVelocity *= damper;
    }
}