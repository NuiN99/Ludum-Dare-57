using UnityEngine;

public class SpearThrowing : MonoBehaviour
{
    public bool HasSpear { get; private set; }
    
    [SerializeField] Projectile spear;
    [SerializeField] Transform spearParent;

    public void Throw(Vector3 force, int damage)
    {
        spear.TogglePhysics(true);
        spear.ToggleRotation(true);
        spear.transform.SetParent(null);
        spear.Launch(force, damage, OnHit_Callback);
        
        HasSpear = false;
    }

    public void RetrieveSpear()
    {
        spear.transform.SetParent(spearParent);
        spear.TogglePhysics(false);
        
        HasSpear = true;
    }

    void OnHit_Callback(Collision collision, int damage)
    {
        spear.ToggleRotation(false);
        
        if (collision.collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}