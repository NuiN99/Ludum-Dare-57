using NuiN.NExtensions;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField, InjectComponent] public SpearThrowing SpearThrowing { get; private set; }
    [field: SerializeField, InjectComponent] public PlayerMovement Movement { get; private set; }
}