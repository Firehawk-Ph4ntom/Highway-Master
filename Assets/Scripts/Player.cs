using UnityEngine;
using System.Collections;

// Serializable class to hold the Obstacle Prefab data
[System.Serializable]
public class ObstacleSpawnData
{
    public GameObject prefab;
    public float spawnWeight = 1.0f;
}

public class Player : MonoBehaviour
{
    public float laneMoveSpeed = 8.0f;
    public float[] lanePositions = { -2.8f, 0f, 2.8f };

    private int currentLane = 1;
    private bool isMovingLane = false;

    public ObstacleSpawnData[] obstacleSpawnData;
    public int minObstaclesPerWave = 1, maxObstaclesPerWave = 2;

    public float initialSpawnDelay = 2.0f;
    public float minSpawnInterval = 1.0f, maxSpawnInterval = 2.0f;

    private float spawnY, destroyY;
    public float spawnOffset = 1.0f, destroyOffset = 1.0f;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        // Calculate the Camera bounds and set the spawnpoint and destroy point of the Obstacles used below
        float worldHeight = Camera.main.orthographicSize * 2.0f;

        float topYPosition = Camera.main.transform.position.y + worldHeight / 2.0f;
        float bottomYPosition = Camera.main.transform.position.y - worldHeight / 2.0f;

        spawnY = topYPosition + spawnOffset;
        destroyY = bottomYPosition - destroyOffset;

        currentLane = Mathf.Clamp(currentLane, 0, lanePositions.Length - 1);

        // Obstacle spawn positions clamped to the lane position array
        transform.position = new Vector3(lanePositions[currentLane], transform.position.y, transform.position.z);

        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if (!FindFirstObjectByType<GameManager>().gameOver)
            MovePlayer();
    }

    private void MovePlayer()
    {
        // Player can only move Left or Right
        if (!isMovingLane)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                currentLane--;

            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                currentLane++;

            currentLane = Mathf.Clamp(currentLane, 0, lanePositions.Length - 1);
        }

        // Player movement is clamped to Lane positions set
        Vector3 targetPosition = new Vector3(lanePositions[currentLane], transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneMoveSpeed * Time.deltaTime);

        // The player movement can only be changed when the player is not currently moving to a Lane already
        isMovingLane = Mathf.Abs(transform.position.x - targetPosition.x) > 0.01f;
    }

    // Subroutine that handles Obstacle wave spawning based on delays set
    private IEnumerator SpawnRoutine()
    {
        // A bit of an initial delay before the first wave starts
        yield return new WaitForSeconds(initialSpawnDelay);

        while (!FindFirstObjectByType<GameManager>().gameOver)
        {
            SpawnWave();
            float delay = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(delay);
        }
    }

    // Each wave, a random Lane is selected for Object spawning
    private void SpawnWave()
    {
        // pick a random Lane to spawn obstacles on
        int safeLane = Random.Range(0, lanePositions.Length);
        int obstacleCount = GetObstacleCount();

        // If a Lane has already been selected for spawning, or if it's the safe lane,
        // then skip it if not all Obstacles have been spawned yet
        bool[] usedLanes = new bool[lanePositions.Length];
        usedLanes[safeLane] = true;

        // Spawn Obstacles until the count is reached, checking for used Lanes and Safe Lanes (Where the Player can actually dodge)
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

    // Minimum of 1 Obstacle, maximum of 2, and always have a Safe Lane for the Player to dodge through
    private int GetObstacleCount()
    {
        int count = Random.Range(minObstaclesPerWave, maxObstaclesPerWave + 1);
        return Mathf.Clamp(count, 1, lanePositions.Length - 1);
    }

    // Obstacles spawn in outside the World Border, which then move downwards on the Y-axis
    // If the Obstacle passes the destroyY point (which is outside the world border from the Player's side),
    // Destroy the Object
    private void SpawnObstacleAt(float xPosition)
    {
        // Pick a Weight first before spawning
        GameObject prefab = PickWeightedObstacle();

        Vector3 spawnPosition = new Vector3(xPosition, spawnY, 0.0f);
        GameObject obstacleObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        Obstacle obstacle = obstacleObject.GetComponent<Obstacle>();
        obstacle.destroyY = destroyY;
    }

    // Weighted system, allowing objects to be prioritized based on a number assigned
    // which the higher the number, the likelier it is to spawn compared to the other Objects
    private GameObject PickWeightedObstacle()
    {
        // Get total spawn weight
        float totalWeight = GetTotalSpawnWeight();

        // If no weight set, then won't spawn anything
        if (totalWeight <= 0.0f)
            return null;

        // Randomized interval between 0 and total weight, which then a range is created for each Object
        // If the range falls within the random value, then that Object spawns
        float randomValue = Random.Range(0.0f, totalWeight);

        for (int i = 0; i < obstacleSpawnData.Length; i++)
        {
            ObstacleSpawnData data = obstacleSpawnData[i];

            // If Object weight is 0, then it shouldn't spawn at all
            if (data.spawnWeight <= 0.0f)
                continue;

            // The range calculation
            randomValue -= data.spawnWeight;

            if (randomValue <= 0.0f)
                return data.prefab;
        }
        return null;
    }

    // Based on the Inspector, the total weight is calculated by just iterating through the numbers
    // of all the object prefabs and summing them up, which is then used for the weighted randomization
    private float GetTotalSpawnWeight()
    {
        float totalWeight = 0.0f;

        for (int i = 0; i < obstacleSpawnData.Length; i++)
        {
            ObstacleSpawnData data = obstacleSpawnData[i];

            if (data.spawnWeight > 0.0f)
                totalWeight += data.spawnWeight;
        }

        return totalWeight;
    }

    // Collision Trigger Detection between a Player and an Obstacle
    private void OnTriggerEnter2D(Collider2D collidee)
    {
        if (!FindFirstObjectByType<GameManager>().gameOver) {

            if (collidee.CompareTag("Obstacle"))
            {
                StopCoroutine(spawnCoroutine);
                FindFirstObjectByType<GameManager>().GameOver();
            }
        }
    }
}