using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float startY, resetY;

    // Background moves downwards in the scene
    // Reset position back to startY once resetY threshold has passed
    private void Update()
    {
        if (GameManager.Instance.gameStarted && !GameManager.Instance.gameOver)
        {
            transform.Translate(new Vector2(0, -GameManager.Instance.worldSpeed * Time.deltaTime));

            if (transform.position.y <= resetY)
            {
                Vector3 position = transform.position;
                position.y = startY;
                transform.position = position;
            }
        }
    }
}