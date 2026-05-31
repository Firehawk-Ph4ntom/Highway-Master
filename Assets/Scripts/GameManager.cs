using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;

    private int score = 0;
    public float worldSpeed = 10.0f;
    
    public bool gameStarted = false;
    public bool gameOver  = false;
    public float gameOverDelay = 3.0f;
    
    public AudioEventData scoreSound;

    private void Awake()
    {
        // Make sure only ONE GameManager exists at any given moment
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        score = 0;
        gameStarted = false;
        gameOver = false;
        scoreText.text = "Score: " + score;
    }

    // Add Score and update the Score Text
    public void AddScore()
    {
        if (gameStarted && !gameOver)
        {
            score++;
            scoreText.text = "Score: " + score;
            AudioManager.Instance.Play(scoreSound);
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
        AmbientStreamPlayer.Instance.FadeOut();
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("GameOverScene");
    }
}