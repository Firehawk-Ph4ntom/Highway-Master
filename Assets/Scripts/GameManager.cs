using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public bool gameOver = false;

    private void Start()
    {
        score = 0;
        gameOver = false;
    }

    void Update()
    {
        
    }
}
