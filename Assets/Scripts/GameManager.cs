using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public bool gameOver = false;

    private void Start()
    {
        score = 0;
        gameOver = false;
        scoreText.text = "Score: 0";
    }

    public void AddScore()
    {
        if (!gameOver)
        {
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    // Set the Final Score to the Current Score, then load the Game Over Scene, which then calls Final Score
    public void GameOver()
    {
        if (!gameOver) {

            gameOver = true;
            PlayerPrefs.SetInt("FinalScore", score);
            SceneManager.LoadScene("GameOverScene");
        }
    }
}