using Modules.Rendering.Outline;
using NuiN.NExtensions;
using UnityEngine;

public class Part : MonoBehaviour, IInteractable
{
    public enum Type { Sonar, Propeller, Torpedo }
    
    [field: SerializeField] public Type PartType { get; private set; }
    [SerializeField, InjectComponent] Rigidbody rb;
    [SerializeField, InjectComponent] Collider col;
    [SerializeField, InjectComponent] OutlineComponent outline;

    public void Interact(Player player)
    {
        if (player.Interaction.HeldPart != null) return;
        
        rb.isKinematic = true;
        col.enabled = false;
        outline.enabled = false;
        player.Interaction.SetHeldPart(this);
    }

    public void Release()
    {
        transform.parent = null;
        rb.isKinematic = false;
        col.enabled = true;
        outline.enabled = true;
    }
}
