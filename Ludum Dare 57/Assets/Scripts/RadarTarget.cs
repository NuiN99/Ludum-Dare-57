using UnityEngine;

public class RadarTarget : MonoBehaviour
{
    [field: SerializeField, Range(0f, 1f)] public float SignalStrength { get; private set; }
}
