using UnityEngine;

public interface IDamageable
{
    public Vector3 Position { get; }
    public void TakeDamage(int damage, Vector3 direction);
}