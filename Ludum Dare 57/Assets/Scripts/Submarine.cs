using System.Collections.Generic;
using Modules.Rendering.Outline;
using UnityEngine;

public class Submarine : MonoBehaviour, IDamageable
{
    public bool IsDead { get; set; }
    public bool IsDamageableCrit => false;
    public EntityType Type => EntityType.Player;
    public Vector3 Position => transform.position;
    
    [SerializeField] List<RepairableSubmarinePart> repairableParts;
    [SerializeField] OutlineComponent[] outlines;
    [SerializeField] GameObject[] repairedVisuals;
    [SerializeField] PilotableSubmarine pilotableSubmarine;
    
    [SerializeField] FMODSoundPlayer repairedPartSound;
    
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

    void Start()
    {
        List<RepairableSubmarinePart> partsToRepair = new();
        
        foreach (RepairableSubmarinePart repairablePart in repairableParts)
        {
            if (GameStateManager.Instance.CollectedParts.Contains(repairablePart.PartType))
            {
                partsToRepair.Add(repairablePart);
            }
        }

        foreach (RepairableSubmarinePart part in partsToRepair)
        {
            part.Repair();
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
        repairedPartSound.PlayAtPosition(part.transform.position);
        
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
        
        pilotableSubmarine.SetRepaired();
        
        _isRepaired = true;
    }

    void ToggleOutlines(bool isOn)
    {
        foreach (var outline in outlines)
        {
            outline.enabled = isOn;
        }
    }
    void IDamageable.TakeDamage(int damage, Vector3 direction)
    {
        Debug.LogError("Sub took damage:" + damage);
        GameEvents.InvokePlayerDied();
    }
}