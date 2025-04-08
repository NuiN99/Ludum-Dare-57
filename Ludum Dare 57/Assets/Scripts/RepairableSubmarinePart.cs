using NuiN.SpleenTween;
using UnityEngine;

public class RepairableSubmarinePart : MonoBehaviour, IInteractable
{
    public Part.Type PartType => requiredPart;
    
    [SerializeField] Part.Type requiredPart;
    [SerializeField] GameObject repairedVisual;
    [SerializeField] ParticleSystem brokenParticles;
    [SerializeField] Collider col;
    
    Submarine _submarine;
    
    void Awake()
    {
        repairedVisual.SetActive(false);
    }

    public void Init(Submarine submarine)
    {
        _submarine = submarine;
    }
    
    public void Interact(Player player)
    {
        if(player.Interaction.HeldPart == null || player.Interaction.HeldPart.PartType != requiredPart) return;

        col.enabled = false;
        
        player.Interaction.DestroyHeldPart();
        
        _submarine.RepairPart(this);
        repairedVisual.SetActive(true);
        SpleenTween.Scale(repairedVisual.transform, Vector3.zero, repairedVisual.transform.localScale, 0.25f).SetEase(Ease.OutElastic);
        brokenParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        
        GameEvents.InvokePartRepaired(requiredPart);
    }

    public void Repair()
    {
        col.enabled = false;
        
        _submarine.RepairPart(this);
        repairedVisual.SetActive(true);
        SpleenTween.Scale(repairedVisual.transform, Vector3.zero, repairedVisual.transform.localScale, 0.25f).SetEase(Ease.OutElastic);
        brokenParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        
        GameEvents.InvokePartRepaired(requiredPart);
    }
}