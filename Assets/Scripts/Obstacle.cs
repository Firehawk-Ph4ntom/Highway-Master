using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float destroyY;
    public Sprite[] spriteVariants;

    private float obstacleSpeed;
    private bool scored = false;

    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameManager = GameManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();

        ApplyRandomSprite();
    }

    private void Update()
    {
        if (gameManager == null || gameManager.gameOver)
            return;

        MoveDown();
        CheckDestroyAndScore();
    }

    private void ApplyRandomSprite()
    {
        if (spriteRenderer == null || spriteVariants == null || spriteVariants.Length == 0)
            return;

        spriteRenderer.sprite = spriteVariants[Random.Range(0, spriteVariants.Length)];
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down * obstacleSpeed * Time.deltaTime, Space.World);
    }

    private void CheckDestroyAndScore()
    {
        if (scored)
            return;

        if (transform.position.y < destroyY)
        {
            scored = true;
            gameManager.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}