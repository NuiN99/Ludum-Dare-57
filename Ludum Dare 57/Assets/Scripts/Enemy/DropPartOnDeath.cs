using UnityEngine;
using UnityEngine.SceneManagement;

public class DropPartOnDeath : MonoBehaviour
{
    [SerializeField] EnemyHealth health;
    [SerializeField] Part partPrefab;
    
    void OnEnable()
    {
        health.OnDeath += SpawnPart;
    }

    void OnDisable()
    {
        health.OnDeath -= SpawnPart;
    }

    void Start()
    {
        if (GameStateManager.Instance.CollectedParts.Contains(partPrefab.PartType))
        {
            Destroy(gameObject);
        }
    }

    void SpawnPart()
    {
        if (GameStateManager.Instance.CollectedParts.Contains(partPrefab.PartType))
        {
            return;
        }
        
        Instantiate(partPrefab, transform.position, Quaternion.identity);
    }
}
