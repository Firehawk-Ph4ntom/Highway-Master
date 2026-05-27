using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    public float startY, resetY;

    private void Update()
    {
        if (!FindFirstObjectByType<GameManager>().gameOver)
        {
            transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime, Space.World);

            if (transform.position.y <= resetY)
            {
                Vector3 position = transform.position;
                position.y = startY;
                transform.position = position;
            }
        }
    }
}