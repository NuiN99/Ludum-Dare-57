using NuiN.NExtensions;
using UnityEngine;

public class PiranhaSpawner : MonoBehaviour
{
    [SerializeField] float initialDelay;
    
    [SerializeField] float minSpawnInterval;
    [SerializeField] float maxSpawnInterval;

    [SerializeField] float minSpawnIntervalProgressed;
    [SerializeField] float maxSpawnIntervalProgressed;
    
    [SerializeField] int minSpawnCount;
    [SerializeField] int maxSpawnCount;
    
    [SerializeField] int minSpawnCountProgressed;
    [SerializeField] int maxSpawnCountProgressed;
    
    [SerializeField] float minSpawnDist;
    [SerializeField] float maxSpawnDist;
    
    [SerializeField] Enemy piranhaPrefab;
    
    TimerRandom _spawnTimer;

    bool _isInitiallyDelayed = true;

    void Awake()
    {
        this.DoAfter(initialDelay, () =>
        {
            _spawnTimer = new TimerRandom(minSpawnInterval, maxSpawnInterval);
            _isInitiallyDelayed = false;
        });
    }

    void Update()
    {
        if (_isInitiallyDelayed || GameStateManager.Instance.LeviathanIsActive || GameStateManager.Instance.HasRepairedSubmarine)
        {
            return;
        }
        
        if (_spawnTimer.IsComplete)
        {
            if (GameStateManager.Instance.HasCollectedFirstPart)
            {
                _spawnTimer = new TimerRandom(minSpawnIntervalProgressed, maxSpawnIntervalProgressed);
                SpawnPiranhas(Random.Range(minSpawnCountProgressed, maxSpawnCountProgressed + 1));
            }
            else
            {
                SpawnPiranhas(Random.Range( minSpawnCount, maxSpawnCount + 1));
                _spawnTimer.Restart();
            }
        }
    }

    void SpawnPiranhas(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 playerPos = Player.Instance.transform.position;
            Vector3 randSphere = Random.insideUnitSphere;
            randSphere = randSphere.With(y: Mathf.Abs(randSphere.y));
            Vector3 spawnOffset = randSphere.normalized * Random.Range(minSpawnDist, maxSpawnDist);
            Vector3 spawnPos = playerPos + spawnOffset;
            Instantiate(piranhaPrefab, spawnPos, Quaternion.identity, transform);
        }
        
        Debug.Log($"Spawned {count} Piranhas");
    }
}
