using NuiN.NExtensions;
using UnityEngine;

public class LeviathanManager : MonoBehaviour
{
    [SerializeField] Leviathan leviathan;
    [SerializeField] Enemy leviathanAIVersion;
    
    
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
        leviathanAIVersion.gameObject.SetActive(false);
        DespawnLeviathan();
    }

    void OnEnable()
    {
        GameEvents.OnPlayerEnterSubmarine += ChangeStateToAILeviathan;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerEnterSubmarine -= ChangeStateToAILeviathan;
    }

    void ChangeStateToAILeviathan()
    {
        DespawnLeviathan();
        _spawnIntervalTimer = new TimerRandom(float.MaxValue, float.MaxValue);
        leviathanAIVersion.gameObject.SetActive(true);
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
        Vector3 spawnOffset = randSphere.With(y: Mathf.Abs(randSphere.y)).normalized * spawnDistance;
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