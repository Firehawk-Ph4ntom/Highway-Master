using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float destroyY;
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
            transform.Translate(Vector2.down * FindFirstObjectByType<GameManager>().worldSpeed * Time.deltaTime, Space.World);

            if (transform.position.y < destroyY)
            {
                FindFirstObjectByType<GameManager>().AddScore();
                Destroy(gameObject);
            }
        }
    }
}