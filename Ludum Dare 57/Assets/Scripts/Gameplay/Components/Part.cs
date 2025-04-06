using NuiN.NExtensions;
using UnityEngine;

public class Part : MonoBehaviour, IInteractable
{
    [SerializeField, InjectComponent] Rigidbody rb;
    [SerializeField, InjectComponent] Collider col;

    public void Interact(Player player)
    {
        rb.isKinematic = true;
        col.enabled = false;
        player.Interaction.SetHeldPart(this);
    }

    public void Release()
    {
        transform.parent = null;
        rb.isKinematic = false;
        col.enabled = true;
    }
}
