using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpawner : MonoBehaviour
{ // this is in the top ten of hardest scripts ive made
    [Header("Pillar Prefabs")]
    public GameObject greenPillar;   
    public GameObject redPillar;     
    public GameObject purplePillar;  
    [Header("Middle Prefab")]
    public GameObject middlePrefab; // point hitbox
    [Header("Spawn Position")]
    public float spawnX = 12f;
    public float minY = -2f;
    public float maxY = 2f;
    [Header("Gap Settings")]
    public float gapHeight = 12f;
    public float minGapHeight = 6f; 
    public float gapDecreaseAmount = 2f;   // subtract 
    public float gapDecreaseNumber = 100f; // this is the time of when it decreases
    [Header("Spawn Rate Difficulty")]
    public float startSpawnRate = 2f;       
    public float minSpawnRate = 0.8f;  // this is the maxiam spawn rate because if it keeps going down it will start laggingag and break
    public float spawnRateDecrease = 0.05f;  // this decreases the time in spawn rate 
    public float difficultyNumber = 5f;

    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = startSpawnRate;
        InvokeRepeating(nameof(SpawnPillars), 1f, currentSpawnRate);
        InvokeRepeating(nameof(IncreaseDifficulty), difficultyNumber, difficultyNumber);
        InvokeRepeating(nameof(DecreaseGap), gapDecreaseNumber, gapDecreaseNumber); 
    }

    void SpawnPillars()
    {
        int combo = Random.Range(0, 3);

        GameObject bottomPrefab = null;
        GameObject topPrefab = null;

        // Pillar HAS TO EQUAL TO 4
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

        // Random vertical center for the gap for pillars
        float centerY = Random.Range(minY, maxY);

        // Positions for top and bottom pillars 
        Vector3 bottomPos = new Vector3(spawnX, centerY - gapHeight / 2f, 0f);
        Vector3 topPos = new Vector3(spawnX, centerY + gapHeight / 2f, 0f);

        // Quaternion is a 4 corordinate rotation hell
        Instantiate(bottomPrefab, bottomPos, Quaternion.identity); // 
        Instantiate(topPrefab, topPos, Quaternion.identity);

        // This makes the middle prefab aka pint spawn in the middle of pillars THIS WAS HELL MAKING A LOT OF RESEARCH FOR
        if (middlePrefab != null)
        {
            Vector3 middlePos = new Vector3(spawnX, centerY, 0f); // exact center ; this was uneccary i coouldve just spawned it and made it long but it was already to late
            Instantiate(middlePrefab, middlePos, Quaternion.identity); // this copies middle prefab at middle postition, then at the confusing quaternion that took to much time.
        }
    }

    void IncreaseDifficulty()
    {
        currentSpawnRate -= spawnRateDecrease;
        currentSpawnRate = Mathf.Max(currentSpawnRate, minSpawnRate); // this gets one of the bigger numbers of the two fartblop
        CancelInvoke(nameof(SpawnPillars)); // this cancels invoke
        InvokeRepeating(nameof(SpawnPillars), 0f, currentSpawnRate); //Invoke calls a function after certain amout of time durrr
    }
    void DecreaseGap()
    {
        gapHeight -= gapDecreaseAmount;
        gapHeight = Mathf.Max(gapHeight, minGapHeight);
    }
}
