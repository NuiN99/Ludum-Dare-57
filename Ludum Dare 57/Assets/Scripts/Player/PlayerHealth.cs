using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public EntityType Type => EntityType.Player;
    public Vector3 Position => transform.position;
    public void TakeDamage(int damage, Vector3 direction)
    {
        Debug.Log($"Player took {damage} damage");
    }
}