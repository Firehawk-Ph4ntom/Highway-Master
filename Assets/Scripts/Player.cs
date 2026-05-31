using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Serializable class to hold the Obstacle Prefab data
[System.Serializable]
public class ObstacleSpawnData
{
    public GameObject prefab;
    public float spawnWeight = 1.0f;
}

// Another Serializable class to hold the Crash Sound data for each Obstacle type
[System.Serializable]
public class CrashSoundData
{
    public string obstacleTag;
    public MultiSoundData sound;
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

    public CrashSoundData[] crashSounds;
    // Create a Dictionary to map Obstacle Tags to their corresponding Crash MultiSound Data
    private readonly Dictionary<string, MultiSoundData> crashSoundMap = new();
    
    private Animator animator;

    private void Awake()
    {
        // Populate the Dictionary
        for (int i = 0; i < crashSounds.Length; i++)
        {
            CrashSoundData data = crashSounds[i];
            crashSoundMap[data.obstacleTag] = data.sound;
        }
    }

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

        animator = GetComponent<Animator>();

        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if (GameManager.Instance.gameStarted && !GameManager.Instance.gameOver)
            MovePlayer();
    }

    private void MovePlayer()
    {
        // Player can only move Left or Right
        // Play Turn Left and Right animations
        if (GameManager.Instance.gameStarted && !isMovingLane && !GameManager.Instance.gameOver)
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && currentLane > 0)
            {
                currentLane--;
                animator.SetTrigger("TURN_LEFT");
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && currentLane < lanePositions.Length - 1)
            {
                currentLane++;
                animator.SetTrigger("TURN_RIGHT");
            }
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

        // Make sure gameStarted is true before executing
        while (!GameManager.Instance.gameStarted)
            yield return null;

        // A bit of an initial delay before the first wave starts
        yield return new WaitForSeconds(initialSpawnDelay);

        while (!GameManager.Instance.gameOver)
        {
            SpawnWave();
            float delay = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(delay);
        }
    }

    // Each wave, one random lane is selected as the safe lane,
    // then obstacles are spawned in the remaining lanes
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

            // If the index lands on the safe lane or on a used lane, don't spawn an Obstacle
            if (usedLanes[laneIndex])
                continue;

            // Set the lane to used
            usedLanes[laneIndex] = true;
            SpawnObstacleAt(lanePositions[laneIndex]);
            spawned++;
        }
    }

    // Calculate the maximum amount of Obstacles to spawn, and always have a Safe Lane for the Player to dodge through
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

        // Randomized interval between 0 and total weight
        float randomValue = Random.Range(0.0f, totalWeight);

        for (int i = 0; i < obstacleSpawnData.Length; i++)
        {
            ObstacleSpawnData data = obstacleSpawnData[i];

            // If Object weight is 0, then it shouldn't spawn at all/chosen for calculation
            if (data.spawnWeight <= 0.0f)
                continue;

            // The range calculation
            randomValue -= data.spawnWeight;

            // The Object that passes the negative check first will be the one that gets spawned
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
        if (GameManager.Instance.gameStarted && !GameManager.Instance.gameOver) {

            string collideeTag = collidee.tag;

            // Each Obstacle generates a different Crash Type, 
            // which is then saved for different Game Over Menu UI Screens and different crash Audio
            if (collideeTag == "HoleObstacle")
            {
                PlayerPrefs.SetString("CrashType", "Hole");
            }
            else if (collideeTag == "BarrelObstacle")
            {
                PlayerPrefs.SetString("CrashType", "Barrel");
            }
            else
            {
                PlayerPrefs.SetString("CrashType", "Vehicle");
            }

            MultiSoundData sound = GetCrashSound(collideeTag);
            AudioManager.Instance.Play(sound);

            // Then, stop Obstacle Spawning subroutine and trigger Game Over in the Game Manager
            // The Game Over Screen will then change depending on the saved Crash Type
            StopCoroutine(spawnCoroutine);
            GameManager.Instance.GameOver();
        }
    }

    // Get the Multisound Data attached to Object/Obstacle Tag and return it back
    private MultiSoundData GetCrashSound(string obstacleTag)
    {
        if (crashSoundMap.TryGetValue(obstacleTag, out MultiSoundData sound))
            return sound;

        return null;
    }
}