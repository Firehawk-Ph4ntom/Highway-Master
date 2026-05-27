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
        scoreText.text = "Score: " + score;
    }

    public void AddScore()
    {
        if (!gameOver) {
            score++;
            scoreText.text = "Score: " + score;
        }
    }
}
