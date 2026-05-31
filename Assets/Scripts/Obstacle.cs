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
        if (GameManager.Instance.gameStarted && !GameManager.Instance.gameOver)
        {
            transform.Translate(new Vector2(0, -GameManager.Instance.worldSpeed * Time.deltaTime));

            if (transform.position.y < destroyY)
            {
                GameManager.Instance.AddScore();
                Destroy(gameObject);
            }
        }
    }
}