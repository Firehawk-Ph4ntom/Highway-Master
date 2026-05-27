using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    public float startY, resetY;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (gameManager == null || gameManager.gameOver)
            return;

        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime, Space.World);

        if (transform.position.y <= resetY)
        {
            transform.position = new Vector3(
                transform.position.x,
                startY,
                transform.position.z
            );
        }
    }
}