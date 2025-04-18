using System;
using NuiN.NExtensions;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField, InjectComponent] public Rigidbody RB { get; private set; }
    [field: SerializeField, InjectComponent] public Collider Col { get; private set; }
    [SerializeField] float rotationSpeed;
    
    Action<Collision, Projectile, int> _onHit;
    int _damage;
    bool _rotate = true;
    
    public void Launch(Vector3 force, int damage, Action<Collision, Projectile, int> onHit)
    {
        transform.rotation = Quaternion.LookRotation(force.normalized);
        _onHit = onHit;
        _damage = damage;
        RB.linearVelocity = force;
    }

    public void TogglePhysics(bool enabled)
    {
        RB.isKinematic = !enabled;
        
        if (!RB.isKinematic)
        {
            ResetVelocity();
        }
    }

    public void ResetVelocity()
    {
        RB.linearVelocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
    }

    public void ToggleRotation(bool rotate)
    {
        _rotate = rotate;
    }

    void OnCollisionEnter(Collision other)
    {
        _onHit?.Invoke(other, this, _damage);
        _onHit = null;
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
        Quaternion targetRotation = Quaternion.LookRotation(RB.linearVelocity.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}