using NuiN.NExtensions;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 HeadPosition => head.position;
    public bool IsDead { get; private set; }
    public bool IsCheckingRadar { get; private set; }

    [field: SerializeField] public PlayerStats Stats { get; private set; }
    [field: SerializeField, InjectComponent] public SpearThrowing SpearThrowing { get; private set; }
    [field: SerializeField, InjectComponent] public PlayerMovement Movement { get; private set; }
    
    public PriorityAnimator PriorityAnimator { get; private set; }

    [SerializeField] Animator animator;
    [SerializeField] Transform head;

    void Awake()
    {
        PriorityAnimator = new PriorityAnimator(animator);
    }

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