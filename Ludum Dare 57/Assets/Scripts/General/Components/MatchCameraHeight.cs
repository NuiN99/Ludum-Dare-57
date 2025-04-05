using NuiN.NExtensions;
using UnityEngine;

public class MatchCameraHeight : MonoBehaviour
{
    float _baseHeight;

    void Start()
    {
        _baseHeight = transform.position.y;
    }

    void LateUpdate()
    {
        transform.position = transform.position.With(y: _baseHeight + PlayerCamera.Instance.CinemachineCam.transform.position.y);
    }
}
