using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float startY, resetY;

    private void Start()
    {

    }

    private void Update()
    {
        if (FindFirstObjectByType<GameManager>().gameStarted && !FindFirstObjectByType<GameManager>().gameOver)
        {
            transform.Translate(Vector2.down * FindFirstObjectByType<GameManager>().worldSpeed * Time.deltaTime, Space.World);

            if (transform.position.y <= resetY)
            {
                Vector3 position = transform.position;
                position.y = startY;
                transform.position = position;
            }
        }
    }
}