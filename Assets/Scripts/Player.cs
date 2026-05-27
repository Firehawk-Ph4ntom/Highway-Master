using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float spawnOffset = 1.0f;
    public float destroyOffset = 1.0f;
    
    private void Start()
    {
        float worldHeight = Camera.main.orthographicSize * 2.0f;

        float topY = Camera.main.transform.position.y + worldHeight / 2.0f;
        float bottomY = Camera.main.transform.position.y - worldHeight / 2.0f;

        spawnY = topY + spawnOffset;
        destroyY = bottomY - destroyOffset;
    }

    void Update()
    {
        
    }
}
