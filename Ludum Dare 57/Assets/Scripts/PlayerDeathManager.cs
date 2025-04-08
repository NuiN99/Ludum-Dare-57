using NuiN.SpleenTween;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathManager : MonoBehaviour
{
    [SerializeField] Image deathFade;
    [SerializeField] float fadeDuration;
    [SerializeField] Ease fadeEase;

    bool _isGameOver;
    
    void OnEnable()
    {
        GameEvents.OnPlayerDied += GameOver;
    }
    void OnDisable()
    {
        GameEvents.OnPlayerDied -= GameOver;
    }

    void GameOver()
    {
        if (_isGameOver) return;
        
        _isGameOver = true;
        SpleenTween.ImageAlpha(deathFade, 0, 1f, fadeDuration).SetEase(fadeEase).OnComplete(() =>
        {
            GameStateManager.Instance.ReloadGame();
        });
    }
}
