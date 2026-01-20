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

    private int currentWave = 0;
    private bool waveInProgress = false;

    void Start()
    {
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            // Wait until no enemies exist in the scene
            yield return new WaitUntil(() =>
                GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !waveInProgress
            );

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
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Assign path points
        EnemyPathAI pathAI = enemy.GetComponent<EnemyPathAI>();
        if (pathAI != null)
        {
            pathAI.SetPath(pathPoints);
        }

        // REQUIRED: enemy prefab must be tagged "Enemy"
        enemy.tag = "Enemy";
    }
}
