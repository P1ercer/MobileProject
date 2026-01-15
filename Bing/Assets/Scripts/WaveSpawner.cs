using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject enemyPrefab;       // Enemy to spawn
    public Transform spawnPoint;          // Where enemies spawn

    [Header("Wave Settings")]
    public int enemiesPerWave = 5;
    public float timeBetweenEnemies = 1f;
    public float timeBetweenWaves = 5f;

    private int currentWave = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            isSpawning = true;

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            isSpawning = false;

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("EnemyPrefab or SpawnPoint not assigned!");
            return;
        }

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
