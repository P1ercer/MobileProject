using System.Collections;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public int amount = 5;          // Enemies per wave
    public float spawnDelay = 0.5f; // Delay between spawns

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private int enemiesSpawned;
    private bool spawning;
    private bool waveActive;

    private void Start()
    {
        StartNewWave();
    }

    private void Update()
    {
        // Spawn enemies for the current wave
        if (waveActive && !spawning && enemiesSpawned < amount)
        {
            StartCoroutine(SpawnWave());
        }

        // Start next wave ONLY when all enemies are gone
        if (waveActive &&
            enemiesSpawned >= amount &&
            GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
        {
            waveActive = false;
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        enemiesSpawned = 0;
        spawning = false;
        waveActive = true;
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
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        Transform chosenPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, chosenPoint.position, Quaternion.identity);
    }
}
