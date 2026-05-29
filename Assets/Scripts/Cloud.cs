using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public Vector2 moveDirection = new Vector2(-0.3f, -1.0f);
    public float destroyY;

    private void Update()
    {
        // Clouds move downward in the scene, and are destroyed on passing the destroyY threshold
        if (FindFirstObjectByType<GameManager>().gameStarted && !FindFirstObjectByType<GameManager>().gameOver)
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);

            if (transform.position.y < destroyY)
            {
                Destroy(gameObject);
            }
        }
    }
}