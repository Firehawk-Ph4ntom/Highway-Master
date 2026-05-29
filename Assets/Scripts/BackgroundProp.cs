using UnityEngine;

public class BackgroundProp : MonoBehaviour
{
    public float destroyY;

    private void Start()
    {

    }

    private void Update()
    {
        // Props move downward in the scene, and are destroyed on passing the destroyY threshold
        // Similar to Obstacle.cs
        if (FindFirstObjectByType<GameManager>().gameStarted && !FindFirstObjectByType<GameManager>().gameOver)
        {
            transform.Translate(Vector2.down * FindFirstObjectByType<GameManager>().worldSpeed * Time.deltaTime, Space.World);

            if (transform.position.y < destroyY)
            {
                Destroy(gameObject);
            }
        }
    }

    // Referenced in BackgroundPropManager
    public void Initialize(float destroyPositionY)
    {
        destroyY = destroyPositionY;
    }
}