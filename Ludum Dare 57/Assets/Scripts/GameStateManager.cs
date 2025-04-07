using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool HasCollectedFirstPart { get; private set; }
    public bool LeviathanIsActive { get; private set; }

    // ReSharper disable once CollectionNeverQueried.Local
    List<Part.Type> _collectedParts = new();

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
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
        _collectedParts.Add(type);
    }

    void OnLeviathanActiveStateChanged(bool isActive)
    {
        LeviathanIsActive = isActive;
    }
}
