using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Camera Shake Options")]
public class CameraShakeOptionsSO : ScriptableObject
{
    [field: SerializeField] public CameraShakeOptions Options { get; private set; }
}