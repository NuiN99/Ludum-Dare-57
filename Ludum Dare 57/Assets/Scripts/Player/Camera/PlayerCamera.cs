using System;
using System.Collections;
using NuiN.NExtensions;
using NuiN.SpleenTween;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 TargetPosition => followTarget.position;
    public static PlayerCamera Instance { get; private set; }
    public static event Action OnPlayerCameraStart = delegate { };
    public Vector3 Forward => CinemachineCam.transform.forward;
    public Vector3 Right => CinemachineCam.transform.right;
    
    [Header("Dependencies")]
    [field: SerializeField] public CinemachineCamera CinemachineCam { get; private set; }
    [field: SerializeField] public Camera Cam { get; private set; }
    [SerializeField] Transform listenerTransform;
    [SerializeField] Transform cameraPivot;
    [SerializeField] Transform cameraTransform;
    [SerializeField] CinemachineBasicMultiChannelPerlin cameraNoise;

    [Header("Options")] 
    [SerializeField, Min(0)] float cameraSpeedX = 0.25f;
    [SerializeField, Min(0)] float cameraSpeedY = 0.25f;
    [SerializeField, Range(-90, 90)] float minLookAngle = -75;
    [SerializeField, Range(-90, 90)] float maxLookAngle = 75;
    
    [Header("Camera Shake")] 
    [SerializeField] float shakeDistanceMax;
    [SerializeField] Ease shakeDistanceEase = Ease.OutQuad;

    [Header("Other")]
    [SerializeField] Transform followTarget;
    
    float _angleY; // horizontal
    float _angleX; // vertical
    
    Vector3 _cameraPosition;
    
    Coroutine _shakeRoutine;

    bool _disableRotation;
    bool _rotateToFollowTarget;
    
    public void SetTrackingTarget(Transform target)
    {
        followTarget = target;
    }

    public void SetLookRotation(Quaternion rotation)
    {
        float targetAngle = rotation.eulerAngles.y;
        float angleDifference = Mathf.DeltaAngle(_angleY, targetAngle);
        SpleenTween.Value(_angleY, _angleY + angleDifference, 0.2f, val => _angleY = val).SetEase(Ease.OutCubic);
    }

    public void EnableRotateToFollowTarget()
    {
        _rotateToFollowTarget = true;
    }

    public void DisableRotation()
    {
        _disableRotation = true;
    }

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        }
        
        Instance = this;
    }

    void Start()
    {
        OnPlayerCameraStart.Invoke();
    }

    void Update()
    {
        if (!_disableRotation)
        {
            _angleX -= InputManager.RotateInput.y * cameraSpeedY;
            _angleX = Mathf.Clamp(_angleX, minLookAngle, maxLookAngle);
            _angleY += (InputManager.RotateInput.x * cameraSpeedX);
        }
    }

    void FixedUpdate()
    {
        RotateCamera();

        listenerTransform.position = CinemachineCam.transform.position;
        listenerTransform.rotation = Quaternion.LookRotation(CinemachineCam.transform.forward.With(y: 0).normalized);
        
        if (followTarget == null) return;
        
        FollowTarget();
    }

    void FollowTarget()
    {
        transform.position = followTarget.position;
    }

    void RotateCamera()
    {
        if (_rotateToFollowTarget)
        {
            cameraTransform.rotation = followTarget.rotation;
            return;
        }
        
        //look
        Vector3 rotation = new Vector3(_angleX, 0, 0);
        Quaternion targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
        
        //pivot
        rotation = new Vector3(0, _angleY, 0);
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;
    }
    
    public void ShakePositional(Vector3 shakePosition, CameraShakeOptions options)
    {
        if (cameraNoise == null || followTarget == null) 
            return;

        float intensity = options.intensity;
        float distance = Vector3.Distance(shakePosition, followTarget.position);
        if (distance > shakeDistanceMax)
        {
            return;
        }
        
        float distanceLerp = SpleenExt.GetEase(distance / shakeDistanceMax, shakeDistanceEase);
        intensity = Mathf.Lerp(intensity, 0, distanceLerp);
        
        // dont override if already shaking with more intensity
        if (intensity <= cameraNoise.AmplitudeGain) 
            return;
        
        this.StopCoroutineSafe(_shakeRoutine);
        _shakeRoutine = StartCoroutine(ShakeForDurationRoutine(intensity, options.duration));
    }
    
    public void Shake(CameraShakeOptions options)
    {
        if (cameraNoise == null) 
            return;

        // dont override if already shaking with more intensity
        if (options.intensity <= cameraNoise.AmplitudeGain) 
            return;
        
        this.StopCoroutineSafe(_shakeRoutine);
        _shakeRoutine = StartCoroutine(ShakeForDurationRoutine(options.intensity, options.duration));
    }

    IEnumerator ShakeForDurationRoutine(float intensity, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float timeLerp = time / duration;
            cameraNoise.AmplitudeGain = Mathf.LerpUnclamped(intensity, 0, SpleenExt.GetEase(timeLerp, Ease.OutCubic));
            yield return null;
        }

        cameraNoise.AmplitudeGain = 0f;
    }
}