using UnityEngine;

public class SpearThrowing : MonoBehaviour
{
    public bool HasSpear { get; private set; }
    
    [SerializeField] Projectile spear;
    [SerializeField] Transform spearParent;

    void Start()
    {
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
        spear.transform.SetParent(spearParent);
        spear.transform.localPosition = Vector3.zero;
        spear.transform.localRotation = Quaternion.identity;
        spear.TogglePhysics(false);
        spear.ToggleRotation(false);
        
        HasSpear = true;
    }

    void OnHit_Callback(Collision collision, int damage)
    {
        spear.ToggleRotation(false);
        spear.RB.linearVelocity *= 0.25f;
        
        if (collision.collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}