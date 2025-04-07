using System.Collections.Generic;
using Modules.Rendering.Outline;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField] List<RepairableSubmarinePart> repairableParts;
    [SerializeField] OutlineComponent[] outlines;

    [SerializeField] GameObject[] repairedVisuals;
    
    bool _isRepaired;

    void Awake()
    {
        ToggleOutlines(false);
        foreach (var part in repairableParts)
        {
            part.Init(this);
        }
        
        foreach (var visual in repairedVisuals)
        {
            visual.gameObject.SetActive(false);
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
            Repair();
        }
    }

    void Repair()
    {
        Debug.Log("Repaired Submarine! : " + _isRepaired);

        foreach (var visual in repairedVisuals)
        {
            visual.gameObject.SetActive(true);
        }
        
        _isRepaired = true;
    }

    void ToggleOutlines(bool isOn)
    {
        foreach (var outline in outlines)
        {
            outline.enabled = isOn;
        }
    }
}