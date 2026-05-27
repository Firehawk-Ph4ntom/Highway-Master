using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;
        PlayerPrefs.SetInt("FinalScore", score);
        SceneManager.LoadScene("GameOverScene");
    }
}