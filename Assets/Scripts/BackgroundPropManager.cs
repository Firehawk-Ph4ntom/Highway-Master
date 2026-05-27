using UnityEngine;
using System.Collections;

public class BackgroundPropManager : MonoBehaviour
{
    public GameObject[] propPrefabs;

    public float spawnY = 7.0f;
    public float destroyY = -7.0f;

    public float minSpawnDelay = 0.4f, maxSpawnDelay = 1.2f;
    public float leftMinX = -5.5f, leftMaxX = -8.5f;
    public float rightMinX = 5.5f, rightMaxX = 8.5f;

    private float xPosition;
    private bool spawnValue;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {

    }

    // Subroutine that handles Prop spawning based on a random delay
    private IEnumerator SpawnRoutine()
    {
        while (!FindFirstObjectByType<GameManager>().gameOver)
        {
            SpawnProp();
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    // Spawn Prop Actual
    // We want the Props to spawn randomly on either side of the screen, so we randomize the X position within two ranges
    private void SpawnProp()
    {
        GameObject prefab = propPrefabs[Random.Range(0, propPrefabs.Length)];
        // The value that determines spawning on the left or right side of the screen
        spawnValue = Random.value < 0.5f;

        if (spawnValue)
        {
            // Spawn on the left side of the screen
            xPosition = Random.Range(leftMinX, leftMaxX);
        }
        else
        {
            // Spawn on the right side of the screen
            xPosition = Random.Range(rightMinX, rightMaxX);
        }

        // Spawn the Prop based on the X position determined and act like an Obstacle in terms of movement
        Vector3 spawnPosition = new Vector3(xPosition, spawnY, 0.0f);
        GameObject propObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        BackgroundProp prop = propObject.GetComponent<BackgroundProp>();

        // Destroy Prop once leaving the screen, similar to Obstacles
        prop.Initialize(destroyY);
    }
}