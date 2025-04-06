using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Player player;
    
    public Part HeldPart { get; private set; }

    void OnEnable()
    {
        InputManager.Controls.Actions.Interact.performed += OnInteractPressed_Callback;
    }

    void OnDisable()
    {
        InputManager.Controls.Actions.Interact.performed -= OnInteractPressed_Callback;
    }

    void OnInteractPressed_Callback(InputAction.CallbackContext ctx)
    {
        RaycastHit[] hits = Physics.SphereCastAll(player.Head.position, player.Stats.InteractRadius, PlayerCamera.Instance.Forward, player.Stats.InteractRange);
        IInteractable closestInteractable = null;
        float closestDistance = Mathf.Infinity;
        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.TryGetComponent(out IInteractable interactable)) continue;
            
            float dist = (hit.point - player.Head.position).sqrMagnitude;
            if (dist >= closestDistance) continue;
            
            closestInteractable = interactable;
            closestDistance = dist;
        }

        closestInteractable?.Interact(player);
    }

    public void SetHeldPart(Part part)
    {
        HeldPart = part;
        part.transform.SetParent(player.Hand);
        part.transform.localPosition = Vector3.zero;
        part.transform.localRotation = Quaternion.identity;
    }

    public void ReleaseHeldPart()
    {
        if (HeldPart != null)
        {
            HeldPart.Release();
            HeldPart = null;
        }
    }

    public void DestroyHeldPart()
    {
        Destroy(HeldPart.gameObject);
    }
    
    void OnDrawGizmos()
    {
        Vector3 start = player.Head.position;
        Vector3 end = player.Head.position + player.Head.forward * player.Stats.InteractRange;
        Gizmos.DrawWireSphere(start, player.Stats.InteractRadius);
        Gizmos.DrawWireSphere(end, player.Stats.InteractRadius);
        Gizmos.DrawLine(start, end);
    }
}
