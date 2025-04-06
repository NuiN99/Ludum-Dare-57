using NuiN.NExtensions;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsDead { get; private set; }

    [field: SerializeField] public PlayerStats Stats { get; private set; }
    [field: SerializeField, InjectComponent] public PlayerSpearHandling SpearHandling { get; private set; }
    [field: SerializeField, InjectComponent] public PlayerMovement Movement { get; private set; }
    [field: SerializeField, InjectComponent] public PlayerInteraction Interaction { get; private set; }
    [field: SerializeField, InjectComponent] public PlayerRadar Radar { get; private set; }
    [field: SerializeField] public Transform Head { get; private set; }
    [field: SerializeField] public Transform Hand { get; private set; }
    [field: SerializeField, InjectComponent] public Collider Col { get; private set; }

    public PriorityAnimator PriorityAnimator { get; private set; }

    [SerializeField] Animator animator;

    void Awake()
    {
        PriorityAnimator = new PriorityAnimator(animator);
    }

    void Start()
    {
        PlayerCamera.Instance.SetTrackingTarget(Head);
        PlayerCamera.Instance.SetLookRotation(Quaternion.LookRotation(Head.forward));
    }

    public void RotateBodyToCamera()
    {
        transform.rotation = Quaternion.LookRotation(PlayerCamera.Instance.Forward.With(y:0).normalized, Vector3.up);
        Head.rotation = Quaternion.LookRotation(PlayerCamera.Instance.Forward);
    }
}