using NuiN.NExtensions;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField, InjectComponent] public SpearThrowing SpearThrowing { get; private set; }
    [field: SerializeField, InjectComponent] public PlayerMovement Movement { get; private set; }

    [SerializeField] Transform head;
    
    void Start()
    {
        PlayerCamera.Instance.SetTrackingTarget(head);
        PlayerCamera.Instance.SetLookRotation(Quaternion.LookRotation(head.forward));
    }

    public void RotateBodyToCamera()
    {
        transform.rotation = Quaternion.LookRotation(PlayerCamera.Instance.Forward.With(y:0).normalized, Vector3.up);
        head.rotation = Quaternion.LookRotation(PlayerCamera.Instance.Forward);
    }
}