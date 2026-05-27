using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    // Display the Final Score on the Game Over Scene
    private void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + finalScore;
        }
    }

    private void Update()
    {

    }

    // Reload the Game Scene
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Go back to the Main Menu Scene
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Quit the Application
    public void QuitGame()
    {
        Application.Quit();
    }
}