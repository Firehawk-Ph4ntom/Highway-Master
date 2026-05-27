using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load the actual Game Scene
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Quit the Application
    public void QuitGame()
    {
        Application.Quit();
    }
}