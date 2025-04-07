using NuiN.NExtensions;
using UnityEngine;

public class LeviathanManager : MonoBehaviour
{
    [SerializeField] Leviathan leviathan;

    [SerializeField] float minSpawnInterval;
    [SerializeField] float maxSpawnInterval;
    [SerializeField] float spawnDistance;

    TimerRandom _spawnIntervalTimer;

    bool _leviathanIsActive;

    void Awake()
    {
        _spawnIntervalTimer = new TimerRandom(minSpawnInterval, maxSpawnInterval);
    }

    void Start()
    {
        DespawnLeviathan();
    }

    void Update()
    {
        if (!_leviathanIsActive && _spawnIntervalTimer.IsComplete)
        {
            SpawnLeviathan();
        }

        if (_leviathanIsActive && leviathan.IsDoneCharging)
        {
            DespawnLeviathan();
        }
    }

    [MethodButton("Spawn Leviathan")]
    void SpawnLeviathan()
    {
        Debug.Log("Leviathan Spawned!");
        
        _spawnIntervalTimer.Restart();
        _leviathanIsActive = true;
        leviathan.gameObject.SetActive(true);

        Vector3 randSphere = Random.insideUnitSphere;
        Vector3 spawnOffset = randSphere.With(y: Mathf.Abs(randSphere.y * 0.25f)).normalized * spawnDistance;
        leviathan.transform.position = Player.Instance.transform.position + spawnOffset;
        
        GameEvents.InvokeLeviathanActiveStateChanged(true);
    }

    void DespawnLeviathan()
    {
        Debug.Log("Leviathan Despawned!");
        
        _spawnIntervalTimer.Restart();
        _leviathanIsActive = false;
        leviathan.gameObject.SetActive(false);
        
        GameEvents.InvokeLeviathanActiveStateChanged(false);
    }
}