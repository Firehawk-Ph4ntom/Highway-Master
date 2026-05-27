using UnityEngine;
using System.Collections;

[System.Serializable]
public class ObstacleSpawnData
{
    public GameObject prefab;
    public float spawnWeight = 1.0f;
}

public class Player : MonoBehaviour
{
    private GameManager gameManager;

    public float laneMoveSpeed = 8.0f;
    public float[] lanePositions = { -2.8f, 0f, 2.8f };

    private int currentLane = 1;
    private bool isMovingLane = false;

    public float worldSpeed = 5.0f;

    public ObstacleSpawnData[] obstacleSpawnData;
    public int minObstaclesPerWave = 1;
    public int maxObstaclesPerWave = 3;

    public float initialSpawnDelay = 2.0f;
    public float minSpawnInterval = 1.0f;
    public float maxSpawnInterval = 3.0f;

    public float spawnOffset = 1.0f;
    public float destroyOffset = 1.0f;

    private float spawnY;
    private float destroyY;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        gameManager = GameManager.Instance;
        Camera cam = Camera.main;

        if (gameManager == null || cam == null)
        {
            Debug.LogError("Missing GameManager or Main Camera.");
            enabled = false;
            return;
        }

        float worldHeight = cam.orthographicSize * 2.0f;

        float topEdge = cam.transform.position.y + worldHeight / 2.0f;
        float bottomEdge = cam.transform.position.y - worldHeight / 2.0f;

        spawnY = topEdge + spawnOffset;
        destroyY = bottomEdge - destroyOffset;

        currentLane = Mathf.Clamp(currentLane, 0, lanePositions.Length - 1);

        transform.position = new Vector3(
            lanePositions[currentLane],
            transform.position.y,
            transform.position.z
        );

        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if (gameManager.gameOver)
            return;

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!isMovingLane)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                currentLane--;

            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                currentLane++;

            currentLane = Mathf.Clamp(currentLane, 0, lanePositions.Length - 1);
        }

        Vector3 targetPosition = new Vector3(
            lanePositions[currentLane],
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            laneMoveSpeed * Time.deltaTime
        );

        isMovingLane = Mathf.Abs(transform.position.x - targetPosition.x) > 0.01f;
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (!gameManager.gameOver)
        {
            SpawnWave();

            float delay = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnWave()
    {
        if (obstacleSpawnData == null || obstacleSpawnData.Length == 0 || lanePositions.Length == 0)
            return;

        int safeLane = Random.Range(0, lanePositions.Length);
        int obstacleCount = GetObstacleCount();

        bool[] usedLanes = new bool[lanePositions.Length];
        usedLanes[safeLane] = true;

        for (int spawned = 0; spawned < obstacleCount;)
        {
            int laneIndex = Random.Range(0, lanePositions.Length);

            if (usedLanes[laneIndex])
                continue;

            usedLanes[laneIndex] = true;
            SpawnObstacleAt(lanePositions[laneIndex]);
            spawned++;
        }
    }

    private int GetObstacleCount()
    {
        int count = Random.Range(minObstaclesPerWave, maxObstaclesPerWave + 1);
        return Mathf.Clamp(count, 1, lanePositions.Length - 1);
    }

    private void SpawnObstacleAt(float xPosition)
    {
        GameObject prefab = PickWeightedObstacle();

        if (prefab == null)
            return;

        Vector3 spawnPosition = new Vector3(xPosition, spawnY, 0.0f);
        GameObject obstacleObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        Obstacle obstacle = obstacleObject.GetComponent<Obstacle>();

        if (obstacle == null)
            return;

        obstacle.SetSpeed(worldSpeed);
        obstacle.destroyY = destroyY;
    }

    private GameObject PickWeightedObstacle()
    {
        float totalWeight = GetTotalSpawnWeight();

        if (totalWeight <= 0.0f)
            return null;

        float randomValue = Random.Range(0.0f, totalWeight);

        for (int i = 0; i < obstacleSpawnData.Length; i++)
        {
            ObstacleSpawnData data = obstacleSpawnData[i];

            if (data.prefab == null || data.spawnWeight <= 0.0f)
                continue;

            randomValue -= data.spawnWeight;

            if (randomValue <= 0.0f)
                return data.prefab;
        }

        return null;
    }

    private float GetTotalSpawnWeight()
    {
        float totalWeight = 0.0f;

        for (int i = 0; i < obstacleSpawnData.Length; i++)
        {
            ObstacleSpawnData data = obstacleSpawnData[i];

            if (data.prefab != null && data.spawnWeight > 0.0f)
                totalWeight += data.spawnWeight;
        }

        return totalWeight;
    }

    public void StopSpawning()
    {
        if (spawnCoroutine == null)
            return;

        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D collidee)
    {
        if (gameManager == null || gameManager.gameOver)
            return;

        if (collidee.CompareTag("Obstacle"))
        {
            StopSpawning();
            gameManager.GameOver();
        }
    }
}