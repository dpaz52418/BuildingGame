using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBlastEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform spawnPositionLeft;
    [SerializeField] private Transform spawnPositionCenter;
    [SerializeField] private Transform spawnPositionRight;

    [SerializeField] private float spawnInterval = 2f; // seconds between spawns

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("no enemy on the spawner");
            return;
        }
        // pick a random position from the three
        Transform[] positions = { spawnPositionLeft, spawnPositionCenter, spawnPositionRight };
        Transform chosen = positions[Random.Range(0, positions.Length)];
        if (chosen == null)
        {
            Debug.LogError("something went wrong with spawning position");
            return;
        }

        Instantiate(enemyPrefab, chosen.position, Quaternion.identity);
    }
}
