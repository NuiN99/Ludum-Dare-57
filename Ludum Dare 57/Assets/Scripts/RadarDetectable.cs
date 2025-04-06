using UnityEngine;

public class RadarDetectable : MonoBehaviour
{
    [field: SerializeField, Range(0f, 1f)] public float SignalStrength { get; private set; }
}
