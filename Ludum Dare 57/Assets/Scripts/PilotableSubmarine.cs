using UnityEngine;

public class PilotableSubmarine : MonoBehaviour, IInteractable
{
    [SerializeField] Transform pilotTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider col;

    bool _isActive;

    void Awake()
    {
        col.enabled = false;
        rb.isKinematic = true;
    }

    public void SetRepaired()
    {
        col.enabled = true;
    }

    public void Interact(Player player)
    {
        if (_isActive) return;
        
        PlayerCamera.Instance.SetTrackingTarget(pilotTransform);
        PlayerCamera.Instance.SetLookRotation(Quaternion.LookRotation(pilotTransform.forward));
        PlayerCamera.Instance.DisableRotation();
        PlayerCamera.Instance.EnableRotateToFollowTarget();

        rb.isKinematic = false;
        _isActive = true;
        player.gameObject.SetActive(false);
    }
}