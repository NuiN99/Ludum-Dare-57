using System.Collections.Generic;
using UnityEngine;

public class SnakeFollow : MonoBehaviour
{
    [Header("Snake Settings")]
    public List<Transform> segments; // First element is the head
    public float followSpeed = 10f;
    public float segmentSpacing = 0.5f;

    List<Vector3> segmentPositions = new List<Vector3>();

    void Start()
    {
        if (segments == null || segments.Count == 0)
        {
            Debug.LogWarning("No segments assigned!");
            return;
        }

        // Initialize segmentPositions with the current positions
        foreach (Transform segment in segments)
        {
            segmentPositions.Add(segment.position);
        }
    }

    void Update()
    {
        if (segments.Count == 0) return;

        // Head follows input or is controlled externally
        segmentPositions[0] = segments[0].position;

        for (int i = 1; i < segments.Count; i++)
        {
            Vector3 targetPosition = segmentPositions[i - 1] - (segments[i - 1].forward * segmentSpacing);
            segmentPositions[i] = Vector3.Lerp(segmentPositions[i], targetPosition, followSpeed * Time.deltaTime);

            segments[i].position = segmentPositions[i];

            // Optional: Rotate to face the segment ahead
            Vector3 direction = segmentPositions[i - 1] - segmentPositions[i];
            if (direction != Vector3.zero)
            {
                segments[i].rotation = Quaternion.Slerp(segments[i].rotation, Quaternion.LookRotation(direction), followSpeed * Time.deltaTime);
            }
        }
    }
}