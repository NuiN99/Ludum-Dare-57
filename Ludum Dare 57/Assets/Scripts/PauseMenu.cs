using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject root;
    bool _isGamePaused = false;

    void Start()
    {
        if (GameStateManager.Instance.CollectedParts.Count <= 0)
        {
            TogglePause();
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
        GameStateManager.Instance.ResetStuff();
    }

    public void TogglePause()
    {
        _isGamePaused = !_isGamePaused;
        Time.timeScale = _isGamePaused ? 0f : 1f;
        root.SetActive(_isGamePaused);

        Cursor.visible = _isGamePaused;
        Cursor.lockState = _isGamePaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void OnEnable()
    {
        InputManager.Controls.Actions.Pause.performed += TogglePause;
    }
    void OnDisable()
    {
        InputManager.Controls.Actions.Pause.performed -= TogglePause;
    }

    void TogglePause(InputAction.CallbackContext ctx)
    {
        TogglePause();
    }
}