using UnityEngine;

public class SpearThrowing : MonoBehaviour
{
    public bool HasSpear { get; private set; }
    
    [SerializeField] Projectile spear;
    [SerializeField] Transform spearParent;

    void Start()
    {
        RetrieveSpear();
    }

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
        spear.transform.localPosition = Vector3.zero;
        spear.TogglePhysics(false);
        spear.ToggleRotation(false);
        
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