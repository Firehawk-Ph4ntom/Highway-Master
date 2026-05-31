using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Vector2 moveDirection = new Vector2(-0.25f, -1.0f);
    public float destroyY;

    private void Update()
    {
        // Clouds move downward in the scene, and are destroyed on passing the destroyY threshold
        if (GameManager.Instance.gameStarted && !GameManager.Instance.gameOver)
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);

            if (transform.position.y < destroyY)
            {
                Destroy(gameObject);
            }
        }
    }
}