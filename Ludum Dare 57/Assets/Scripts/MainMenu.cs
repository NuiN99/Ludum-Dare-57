using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string gameScene;
    
    public void StartGame()
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
