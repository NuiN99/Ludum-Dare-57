using UnityEngine;

public interface IDamageable
{
    public bool IsDead { get; set; }
    public bool IsDamageableCrit { get; }
    public EntityType Type { get; }
    public Vector3 Position { get; }
    public void TakeDamage(int damage, Vector3 direction);
}

public enum EntityType
{
    Player,
    Piranha,
    Leviathan,
}