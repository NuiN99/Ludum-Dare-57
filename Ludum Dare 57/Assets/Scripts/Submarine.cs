using System.Collections.Generic;
using Modules.Rendering.Outline;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField] List<RepairableSubmarinePart> repairableParts;
    [SerializeField] OutlineComponent[] outlines;
    
    bool _isRepaired;

    void Awake()
    {
        ToggleOutlines(false);
        foreach (var part in repairableParts)
        {
            part.Init(this);
        }
    }

    void OnEnable()
    {
        GameEvents.OnPlayerHoldingPartStateChanged += ToggleOutlines;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerHoldingPartStateChanged -= ToggleOutlines;
    }

    public void RepairPart(RepairableSubmarinePart part)
    {
        repairableParts.Remove(part);
     
        if (repairableParts.Count <= 0)
        {
            _isRepaired = true;
            Debug.Log("Repaired Submarine! : " + _isRepaired);
        }
    }

    void ToggleOutlines(bool isOn)
    {
        foreach (var outline in outlines)
        {
            outline.enabled = isOn;
        }
    }
}