using UnityEngine;
using UnityEngine.SceneManagement;

public class DropPartOnDeath : MonoBehaviour
{
    [SerializeField] EnemyHealth health;
    [SerializeField] GameObject partPrefab;
    
    void OnEnable()
    {
        health.OnDeath += SpawnPart;
    }

    void OnDisable()
    {
        health.OnDeath -= SpawnPart;
    }

    void SpawnPart()
    {
        Instantiate(partPrefab, transform.position, Quaternion.identity);
    }
}
