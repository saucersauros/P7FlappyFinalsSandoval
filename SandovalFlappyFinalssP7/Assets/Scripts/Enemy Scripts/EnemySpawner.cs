using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Range")]
    public float minY = 1f;
    public float maxY = 10f;
    public float spawnX = 12f;

    [Header("Spawn Timing")]
    public float spawnDelay = 1.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnDelay);
    }

    void SpawnEnemy()
    {
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}