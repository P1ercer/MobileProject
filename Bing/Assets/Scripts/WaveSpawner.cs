using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    [Header("Path Settings")]
    public Transform[] pathPoints;

    [Header("Wave Settings")]
    public int enemiesPerWave = 5;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 2f;

    private int enemiesAlive = 0;
    private int currentWave = 0;
    private bool waveInProgress = false;

    void Start()
    {
        Debug.Log("WaveSpawner Start() running");
        StartCoroutine(WaveLoop());
    }


    IEnumerator WaveLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => enemiesAlive == 0 && !waveInProgress);

            yield return new WaitForSeconds(timeBetweenWaves);

            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        waveInProgress = true;
        currentWave++;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        waveInProgress = false;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("EnemyPrefab or SpawnPoint not assigned!");
            return;
        }

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        enemiesAlive++;

        // Assign path points
        EnemyPathAI pathAI = enemy.GetComponent<EnemyPathAI>();
        if (pathAI != null)
        {
            pathAI.SetPath(pathPoints);
        }
        else
        {
            Debug.LogWarning("Spawned enemy has no EnemyPathAI!");
        }

        // Hook into death event
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.onDeath += OnEnemyKilled;
        }
    }

    void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
