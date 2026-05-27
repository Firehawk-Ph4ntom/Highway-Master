using UnityEngine;
using System.Collections;

public class BackgroundPropManager : MonoBehaviour
{
    public GameObject[] propPrefabs;

    public float scrollSpeed = 5.0f;

    public float spawnY = 7.0f;
    public float destroyY = -7.0f;

    public float minSpawnDelay = 0.4f;
    public float maxSpawnDelay = 1.2f;

    public float leftMinX = -5.5f;
    public float leftMaxX = -8.5f;

    public float rightMinX = 5.5f;
    public float rightMaxX = 8.5f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (gameManager == null || !gameManager.gameOver)
        {
            SpawnProp();

            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnProp()
    {
        if (propPrefabs == null || propPrefabs.Length == 0)
            return;

        GameObject prefab = propPrefabs[Random.Range(0, propPrefabs.Length)];

        if (prefab == null)
            return;

        bool spawnLeft = Random.value < 0.5f;

        float xPosition = spawnLeft
            ? Random.Range(leftMinX, leftMaxX)
            : Random.Range(rightMinX, rightMaxX);

        Vector3 spawnPosition = new Vector3(xPosition, spawnY, 0.0f);
        GameObject propObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        BackgroundProp prop = propObject.GetComponent<BackgroundProp>();

        if (prop != null)
            prop.Initialize(scrollSpeed, destroyY);
    }
}