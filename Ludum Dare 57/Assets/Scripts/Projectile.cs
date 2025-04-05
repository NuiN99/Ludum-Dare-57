using System;
using NuiN.NExtensions;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, InjectComponent] Rigidbody rb;
    [SerializeField] float rotationSpeed;
    
    Action<Collision, int> _onHit;
    int _damage;
    bool _rotate = true;
    
    public void Launch(Vector3 force, int damage, Action<Collision, int> onHit)
    {
        _onHit = onHit;
        _damage = damage;
        rb.linearVelocity = force;
    }

    public void TogglePhysics(bool enabled)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = !enabled;
    }

    public void ToggleRotation(bool rotate)
    {
        _rotate = rotate;
    }

    void OnCollisionEnter(Collision other)
    {
        _onHit?.Invoke(other, _damage);
    }

    void LateUpdate()
    {
        if (_rotate)
        {
            RotateToVelocity();
        }
    }

    void RotateToVelocity()
    {
        Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}