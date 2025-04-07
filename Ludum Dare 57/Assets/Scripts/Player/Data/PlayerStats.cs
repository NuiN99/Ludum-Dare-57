using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [field: Header("Movement")]
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    [field: SerializeField] public float VelocityDamper { get; private set; } = 0.95f;
    [field: SerializeField] public float Gravity { get; private set; } = 1f;
    [field: SerializeField] public float DashForce { get; private set; } = 5f;
    [field: SerializeField] public float DashCooldown { get; private set; } = 2.5f;
    
    [field: Header("Spear")]
    [field: SerializeField] public float SpearThrowForce { get; private set; }
    [field: SerializeField] public int SpearThrowDamage { get; private set; } = 2;
    [field: SerializeField] public int SpearPokeDamage { get; private set; } = 1;
    [field: SerializeField] public float SpearPokeRange { get; private set; } = 1.5f;
    [field: SerializeField] public float SpearPokeRadius { get; private set; } = 0.5f;
    [field: SerializeField] public float SpearPokeDuration { get; private set; } = 0.5f;
    
    [field: Header("Interaction")]
    [field: SerializeField] public float InteractRange { get; private set; } = 2.5f;
    [field: SerializeField] public float InteractRadius { get; private set; } = 1f;
    
    [field: Header("Health")]
    [field: SerializeField] public int MaxHealth { get; private set; } = 3;
    [field: SerializeField] public int RechargeHealthDuration { get; private set; } = 10;
    [field: SerializeField] public float HitInvicibilityDuration { get; private set; } = 0.5f; 
}