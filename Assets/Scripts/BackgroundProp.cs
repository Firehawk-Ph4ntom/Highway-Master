using UnityEngine;

public class BackgroundProp : MonoBehaviour
{
    public float destroyY;

    private void Update()
    {
        // Props move downward in the scene, and are destroyed on passing the destroyY threshold
        // Similar to Obstacle.cs
        if (GameManager.Instance.gameStarted && !GameManager.Instance.gameOver)
        {
            transform.Translate(new Vector2(0, -GameManager.Instance.worldSpeed * Time.deltaTime));

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