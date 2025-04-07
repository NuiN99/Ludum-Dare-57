using System.Linq;
using NuiN.NExtensions;
using NuiN.ScriptableHarmony;
using NuiN.ScriptableHarmony.Core;
using UnityEngine;

public class PlayerRadar : MonoBehaviour
{
    public bool IsOpen { get; private set; }
    
    [SerializeField] float maxDistance = 50f;
    [SerializeField] float minDistance = 5f;

    [SerializeField] float minBlinkInterval = 0.1f;
    [SerializeField] float maxBlinkInterval = 5f;
    [SerializeField] float blinkDuration;
    [SerializeField] float overrideDirectionSpinSpeed = 5f;

    [SerializeField] GetRuntimeSet<RadarTarget> detectableTargets;

    [SerializeField] Transform directionIndicator;
    [SerializeField] GameObject blinkLight;

    [Header("Sounds")] 
    [SerializeField] FMODSoundPlayer clickSound;
    [SerializeField] FMODSoundPlayer openSound;
    [SerializeField] FMODSoundPlayer closeSound;
    
    Timer _blinkDurationTimer;
    float _blinkTime;

    public void OpenRadar()
    {
        _blinkDurationTimer.Restart();
        IsOpen = true;

        openSound.PlayEvent();
    }

    public void CloseRadar()
    {
        IsOpen = false;
        
        closeSound.PlayEvent();
    }

    void Awake()
    {
        _blinkDurationTimer = new Timer(blinkDuration);
    }

    void Start()
    {
        blinkLight.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if(IsOpen == false) return;

        if (detectableTargets.Entities.Any(target => target.OverrideRadar))
        {
            directionIndicator.transform.Rotate(Vector3.up, overrideDirectionSpinSpeed * Time.deltaTime, Space.Self);
            UpdateRadarBlinking(1f);
            return;
        }
        
        RadarTarget closestTarget = null;
        float closestDist = float.MaxValue;

        foreach (RadarTarget target in detectableTargets.Entities)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestTarget = target;
            }
        }

        if (closestTarget == null) return;

        float distLerp = 1f - Mathf.InverseLerp(minDistance, maxDistance, closestDist);
        Vector3 direction = closestTarget.transform.position - transform.position;

        Vector3 localUp = directionIndicator.up;
        direction = Vector3.ProjectOnPlane(direction, localUp);

        if (direction.sqrMagnitude > 0.0001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, localUp);

            Quaternion rotationDelta = Quaternion.Inverse(directionIndicator.rotation) * lookRotation;
            rotationDelta.ToAngleAxis(out float angle, out Vector3 axis);

            if (Vector3.Dot(axis, directionIndicator.up) < 0f)
                angle = -angle;

            directionIndicator.Rotate(Vector3.up, angle, Space.Self);
        }
        
        UpdateRadarBlinking(distLerp);
    }

    void UpdateRadarBlinking(float distLerp)
    {
        if (!_blinkDurationTimer.IsComplete)
        {
            return;
        }
        
        blinkLight.SetActive(false);
        _blinkTime += Time.deltaTime;
        float blinkInterval = Mathf.Lerp(maxBlinkInterval, minBlinkInterval, distLerp);
        if (_blinkTime >= blinkInterval)
        {
            _blinkTime = 0f;
            blinkLight.SetActive(true);
            _blinkDurationTimer.Restart();
            
            clickSound.PlayEvent();
        }
    }
}
