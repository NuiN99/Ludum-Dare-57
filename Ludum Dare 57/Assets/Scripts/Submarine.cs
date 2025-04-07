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
    }
}