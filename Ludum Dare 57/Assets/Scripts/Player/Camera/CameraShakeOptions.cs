using System;
using UnityEngine;

[Serializable]
public struct CameraShakeOptions
{
    public float intensity;
    public float duration;
    
    public CameraShakeOptions(float intensity, float duration)
    {
        this.intensity = intensity;
        this.duration = duration;
    }
    
    public CameraShakeOptions WithMult(float intensityMult)
    {
        float newIntensity = Mathf.Lerp(0, intensity, intensityMult);
        float newDuration =  Mathf.Lerp(0, duration, intensityMult);
        return new CameraShakeOptions(newIntensity, newDuration);
    }
}