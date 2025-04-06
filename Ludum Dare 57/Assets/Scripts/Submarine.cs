using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField] List<RepairableSubmarinePart> repairableParts;

    bool _isRepaired;

    void Awake()
    {
        foreach (var part in repairableParts)
        {
            part.Init(this);
        }
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
}