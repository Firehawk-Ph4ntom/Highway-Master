using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public bool gameOver = false;
    public float worldSpeed = 10.0f;

    private void Start()
    {
        score = 0;
        gameOver = false;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {

    }

    public void AddScore()
    {
        if (!gameOver)
        {
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    // Set the FinalScore to the Current Score, then load the Game Over Scene, which then calls FinalScore
    public void GameOver()
    {
        if (!gameOver) {

            gameOver = true;
            PlayerPrefs.SetInt("FinalScore", score);
            SceneManager.LoadScene("GameOverScene");
        }
    }
}