using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [field: SerializeField] public float SpearThrowForce { get; private set; }
    [field: SerializeField] public int SpearThrowDamage { get; private set; } = 2;
    [field: SerializeField] public int SpearPokeDamage { get; private set; } = 1;
}