using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Player player;

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

    void OnDrawGizmos()
    {
        Vector3 start = player.Head.position;
        Vector3 end = player.Head.position + player.Head.forward * player.Stats.InteractRange;
        Gizmos.DrawWireSphere(start, player.Stats.InteractRadius);
        Gizmos.DrawWireSphere(end, player.Stats.InteractRadius);
        Gizmos.DrawLine(start, end);
    }
}
