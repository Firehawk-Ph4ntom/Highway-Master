using UnityEngine;

public class BackgroundProp : MonoBehaviour
{
    private float moveSpeed;
    private float destroyY;

    public void Initialize(float speed, float destroyPositionY)
    {
        moveSpeed = speed;
        destroyY = destroyPositionY;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);

        if (transform.position.y <= destroyY)
        {
            Destroy(gameObject);
        }
    }
}