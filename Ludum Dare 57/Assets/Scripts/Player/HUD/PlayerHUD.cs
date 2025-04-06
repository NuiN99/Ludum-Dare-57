using NuiN.NExtensions;
using NuiN.SpleenTween;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image reticle;
    [SerializeField] float reticleTweenDuration = 0.25f;
    [SerializeField] Ease reticleTweenEase;
    
    ITween _activeReticleTween;

    void Start()
    {
        reticle.color = reticle.color.WithAlpha(0f);
    }

    void OnEnable()
    {
        GameEvents.OnAimStateChanged += ToggleCrosshair;
    }

    void OnDisable()
    {
        GameEvents.OnAimStateChanged -= ToggleCrosshair;
    }

    void ToggleCrosshair(bool isEnabled)
    {
        _activeReticleTween?.Stop();
        _activeReticleTween = SpleenTween.ImageAlpha(reticle, isEnabled ? 1f : 0f, reticleTweenDuration).SetEase(reticleTweenEase);
    }
}
