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
    
    [field: Header("Interaction")]
    [field: SerializeField] public float InteractRange { get; private set; } = 2.5f;
    [field: SerializeField] public float InteractRadius { get; private set; } = 1f;

}