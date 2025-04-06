using UnityEngine;

public class RepairableSubmarinePart : MonoBehaviour, IInteractable
{
    [SerializeField] Part.Type requiredPart;
    [SerializeField] GameObject repairedVisual;
    [SerializeField] ParticleSystem brokenParticles;

    Submarine _submarine;
    bool _isRepaired = false;
    
    void Start()
    {
        repairedVisual.SetActive(false);
    }

    public void Init(Submarine submarine)
    {
        _submarine = submarine;
    }
    
    public void Interact(Player player)
    {
        if (_isRepaired) return;
        
        if(player.Interaction.HeldPart == null || player.Interaction.HeldPart.PartType != requiredPart) return;
        
        player.Interaction.DestroyHeldPart();
        
        _submarine.RepairPart(this);
        _isRepaired = true;
        repairedVisual.SetActive(true);
        brokenParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}