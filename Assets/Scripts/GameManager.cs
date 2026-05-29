using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public bool gameStarted = false;
    public bool gameOver  = false;
    public float gameOverDelay = 3.0f;
    public float worldSpeed = 10.0f;

    private void Start()
    {
        score = 0;
        gameStarted = false;
        gameOver = false;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {

    }

    // Add Score and update the Score Text
    public void AddScore()
    {
        if (gameStarted && !gameOver)
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
            StartCoroutine(GameOverRoutine());
        }
    }

    // Subroutine to create a small delay before loading the Game Over Scene, in order to fade out
    // all ambient audio for a smoother transition to the Game Over Scene
    private IEnumerator GameOverRoutine()
    {
        FindFirstObjectByType<AmbientStreamPlayer>().FadeOut();
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("GameOverScene");
    }
}