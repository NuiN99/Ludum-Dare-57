using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool HasCollectedFirstPart { get; private set; }
    public bool LeviathanIsActive { get; private set; }
    public bool HasRepairedSubmarine { get; private set; }

    // ReSharper disable once CollectionNeverQueried.Local
    public List<Part.Type> CollectedParts { get; private set; } = new();

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            CollectedParts = new List<Part.Type>();
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void OnEnable()
    {
        GameEvents.OnPartRepaired += OnPartRepaired;
        GameEvents.OnLeviathanActiveStateChanged += OnLeviathanActiveStateChanged;
    }
    void OnDisable()
    {
        GameEvents.OnPartRepaired -= OnPartRepaired;
        GameEvents.OnLeviathanActiveStateChanged -= OnLeviathanActiveStateChanged;
    }

    void OnPartRepaired(Part.Type type)
    {
        HasCollectedFirstPart = true;
        CollectedParts.Add(type);
        
        if(CollectedParts.Count >= 2) HasRepairedSubmarine = true;
    }

    void OnLeviathanActiveStateChanged(bool isActive)
    {
        LeviathanIsActive = isActive;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Main");
    }
}
