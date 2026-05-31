using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs;

    public float minSpawnInterval = 1.0f, maxSpawnInterval = 3.0f;
    public float spawnOffset = 2.0f, destroyOffset = 2.0f;

    private float spawnY, destroyY;
    private float minX, maxX;

    private void Start()
    {
        // Calculate the Camera bounds and set the spawnpoint and destroy point of the Clouds used below
        float worldHeight = Camera.main.orthographicSize * 2.0f;
        float worldWidth = worldHeight * Camera.main.aspect;

        spawnY = Camera.main.transform.position.y + worldHeight / 2.0f + spawnOffset;
        destroyY = Camera.main.transform.position.y - worldHeight / 2.0f - destroyOffset;

        minX = Camera.main.transform.position.x - worldWidth / 2.0f;
        maxX = Camera.main.transform.position.x + worldWidth / 2.0f;

        StartCoroutine(SpawnCloudRoutine());
    }

    // Subroutine that handles Cloud spawning based on delays set
    private IEnumerator SpawnCloudRoutine()
    {
        // Make sure gameStarted is true before executing
        while (!GameManager.Instance.gameStarted)
            yield return null;

        while (!GameManager.Instance.gameOver)
        {
            SpawnCloud();
            float delay = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(delay);
        }
    }

    // Spawn the Clouds
    private void SpawnCloud()
    {
        // Spawn Clouds randomly, no need for a weight system here
        GameObject prefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];

        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnY, 0.0f);
        GameObject cloudObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        Cloud cloud = cloudObject.GetComponent<Cloud>();

        // referenced in Cloud script
        cloud.destroyY = destroyY;
    }
}