using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// Different enum values for different Crash Types
public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    public Image GameOver;
    public Sprite GameOver_Hole;
    public Sprite GameOver_Barrel;
    public Sprite GameOver_Crash;

    // Display the Final Score on the Game Over Scene and change the background image based on the Crash Type
    private void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        string crashType = PlayerPrefs.GetString("CrashType", "Vehicle");

        finalScoreText.text = "Final Score: " + finalScore;

        if (crashType == "Hole")
            GameOver.sprite = GameOver_Hole;
        else if (crashType == "Barrel")
            GameOver.sprite = GameOver_Barrel;
        else
            GameOver.sprite = GameOver_Crash;
    }
}