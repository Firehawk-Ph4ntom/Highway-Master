using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float destroyY;
    private float obstacleSpeed;
    public Sprite[] spriteVariants;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        // Apply randomized Sprites to the Object Prefab instead of defining too many new Prefabs
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteVariants[Random.Range(0, spriteVariants.Length)];
    }

    private void Update()
    {
        // Obstacles move downward in the scene, and are destroyed on passing the destroyY threshold
        if (!FindFirstObjectByType<GameManager>().gameOver)
        {
            transform.Translate(Vector2.down * obstacleSpeed * Time.deltaTime, Space.World);

            if (transform.position.y < destroyY)
            {
                FindFirstObjectByType<GameManager>().AddScore();
                Destroy(gameObject);
            }
        }
    }

    // Referenced in Player class
    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}