using NuiN.NExtensions;
using UnityEngine;

public class LeviathanWinEvent : MonoBehaviour
{
    [SerializeField] EnemyHealth health;
    void OnEnable()
    {
        health.OnDeath += Win;
    }

    void OnDisable()
    {
        health.OnDeath -= Win;
    }

    void Win()
    {
        GameEvents.InvokePlayerKilledLeviathan();
    }

    [MethodButton("Kill")]
    void Kill()
    {
        health.TakeDamage(9999999, Vector3.zero);
    }
}
