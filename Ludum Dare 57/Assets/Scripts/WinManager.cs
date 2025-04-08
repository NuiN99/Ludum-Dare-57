using NuiN.NExtensions;
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
        this.DoAfter(5f, () =>
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            root.SetActive(true);
        });
    }
}