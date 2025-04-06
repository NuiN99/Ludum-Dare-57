using UnityEngine;

public class MatchCameraPosition : MonoBehaviour
{
    void LateUpdate()
    {
        transform.position = PlayerCamera.Instance.CinemachineCam.transform.position;
    }
}