using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    [SerializeField] GameObject root;
    
    void OnEnable()
    {
        GameEvents.OnPlayerKilledLeviathan += ActivateWinScreen;
    }
    void OnDisable()
    {
        GameEvents.OnPlayerKilledLeviathan -= ActivateWinScreen;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        GameStateManager.Instance.ResetStuff();
    }

    void ActivateWinScreen()
    {
        Time.timeScale = 0f;
        root.SetActive(true);
    }
}