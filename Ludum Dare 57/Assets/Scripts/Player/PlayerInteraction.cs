using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float range;
    [SerializeField] float radius;

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
        RaycastHit[] hits = Physics.SphereCastAll(player.HeadPosition, radius, PlayerCamera.Instance.Forward, range);
        IInteractable closestInteractable = null;
        float closestDistance = Mathf.Infinity;
        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.TryGetComponent(out IInteractable interactable)) continue;
            
            float dist = (hit.point - player.HeadPosition).sqrMagnitude;
            if (dist >= closestDistance) continue;
            
            closestInteractable = interactable;
            closestDistance = dist;
        }

        closestInteractable?.Interact(player);
    }

    void OnDrawGizmos()
    {
        if (PlayerCamera.Instance == null) return;
        
        Vector3 start = player.HeadPosition;
        Vector3 end = player.HeadPosition + PlayerCamera.Instance.Forward * range;
        Gizmos.DrawSphere(start, radius);
        Gizmos.DrawSphere(end, radius);
        Gizmos.DrawLine(start, end);
    }
}
