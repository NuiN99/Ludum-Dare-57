using NuiN.NExtensions;
using UnityEngine;

public class MatchCameraPosition : MonoBehaviour
{
    [SerializeField] float interval;

    Timer _intervalTimer;

    void Awake()
    {
        _intervalTimer = new Timer(interval);
    }

    void Update()
    {
        if (_intervalTimer.IsComplete)
        {
            _intervalTimer.Restart();
            transform.position = PlayerCamera.Instance.TargetPosition;
        }
    }
}