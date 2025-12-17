using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpawner : MonoBehaviour
{
    [Header("Pillar Prefabs")]
    public GameObject greenPillar;   // value 1
    public GameObject redPillar;     // value 2
    public GameObject purplePillar;  // value 3

    [Header("Middle Prefab")]
    public GameObject middlePrefab;  // assign your hitbox prefab here

    [Header("Spawn Position")]
    public float spawnX = 12f;
    public float minY = -2f;
    public float maxY = 2f;

    [Header("Gap Settings")]
    public float gapHeight = 12f;
    public float minGapHeight = 6f;
    public float gapDecreaseAmount = 2f;
    public float gapDecreaseInterval = 100f;

    [Header("Spawn Rate Difficulty")]
    public float startSpawnRate = 2f;
    public float minSpawnRate = 0.8f;
    public float spawnRateDecrease = 0.05f;
    public float difficultyInterval = 5f;

    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = startSpawnRate;

        InvokeRepeating(nameof(SpawnPillars), 1f, currentSpawnRate);
        InvokeRepeating(nameof(IncreaseDifficulty), difficultyInterval, difficultyInterval);
        InvokeRepeating(nameof(DecreaseGap), gapDecreaseInterval, gapDecreaseInterval);
    }

    void SpawnPillars()
    {
        int combo = Random.Range(0, 3);

        GameObject bottomPrefab = null;
        GameObject topPrefab = null;

        // Pillar value rules (must equal 4)
        switch (combo)
        {
            case 0: // 3 + 1
                bottomPrefab = purplePillar;
                topPrefab = greenPillar;
                break;

            case 1: // 1 + 3
                bottomPrefab = greenPillar;
                topPrefab = purplePillar;
                break;

            case 2: // 2 + 2
                bottomPrefab = redPillar;
                topPrefab = redPillar;
                break;
        }

        // Random vertical center for the gap
        float centerY = Random.Range(minY, maxY);

        // Positions for top and bottom pillars
        Vector3 bottomPos = new Vector3(spawnX, centerY - gapHeight / 2f, 0f);
        Vector3 topPos = new Vector3(spawnX, centerY + gapHeight / 2f, 0f);

        // Spawn top and bottom pillars
        Instantiate(bottomPrefab, bottomPos, Quaternion.identity);
        Instantiate(topPrefab, topPos, Quaternion.identity);

        // Spawn **exactly one middle hitbox** at the center of the gap
        if (middlePrefab != null)
        {
            Vector3 middlePos = new Vector3(spawnX, centerY, 0f); // exact center
            Instantiate(middlePrefab, middlePos, Quaternion.identity);
        }
    }

    void IncreaseDifficulty()
    {
        currentSpawnRate -= spawnRateDecrease;
        currentSpawnRate = Mathf.Max(currentSpawnRate, minSpawnRate);

        CancelInvoke(nameof(SpawnPillars));
        InvokeRepeating(nameof(SpawnPillars), 0f, currentSpawnRate);
    }

    void DecreaseGap()
    {
        gapHeight -= gapDecreaseAmount;
        gapHeight = Mathf.Max(gapHeight, minGapHeight);
    }
}
