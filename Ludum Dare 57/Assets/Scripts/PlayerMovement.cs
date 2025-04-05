using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float damper;
    [SerializeField] float gravity;
    
    public void Move(Vector3 direction, float speedMult)
    {
        rb.AddForce(direction * moveSpeed * speedMult);
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        rb.linearVelocity *= damper;
    }
}