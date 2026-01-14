using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public int amount = 5;                  // How many enemies to spawn per wave
    public float spawnPadding = 2f;         // Distance outside the screen
    public float spawnDelay = 0.5f;         // Delay between enemy spawns

    [Header("Spawn Control")]
    public Transform spawnPoint;            // Optional manual spawn location

    private Camera cam;
    private int enemiesSpawned;
    private bool spawning;

    private void Start()
    {
        cam = Camera.main;
        StartNewWave();
    }

    private void Update()
    {
        // Start spawning if not already doing so
        if (!spawning && enemiesSpawned < amount)
        {
            StartCoroutine(SpawnWave());
        }

        // When wave is fully spawned AND all enemies are dead, start next wave
        if (enemiesSpawned >= amount &&
            GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        enemiesSpawned = 0;
        spawning = false;
    }

    IEnumerator SpawnWave()
    {
        spawning = true;

        while (enemiesSpawned < amount)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnDelay);
        }

        spawning = false;
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos;

        if (spawnPoint != null)
        {
            spawnPos = spawnPoint.position;
        }
        else
        {
            spawnPos = GetPositionOutsideCamera();
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    Vector3 GetPositionOutsideCamera()
    {
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        int side = Random.Range(0, 4);
        Vector3 pos = Vector3.zero;

        switch (side)
        {
            case 0: // Left
                pos = new Vector3(min.x - spawnPadding, Random.Range(min.y, max.y), 0);
                break;

            case 1: // Right
                pos = new Vector3(max.x + spawnPadding, Random.Range(min.y, max.y), 0);
                break;

            case 2: // Bottom
                pos = new Vector3(Random.Range(min.x, max.x), min.y - spawnPadding, 0);
                break;

            case 3: // Top
                pos = new Vector3(Random.Range(min.x, max.x), max.y + spawnPadding, 0);
                break;
        }

        return pos;
    }
}
