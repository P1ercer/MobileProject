using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Enemies")]
    public GameObject[] enemyPrefabs;
    public int enemyCount = 5;

    [Header("Timing")]
    public float timeBetweenSpawns = 0.5f;
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public Transform[] pathPoints;

    [Header("Wave Settings")]
    public Wave[] waves;

    private int currentWaveIndex = -1;
    private bool waveInProgress = false;
    private bool waitingForButton = true;

    void Update()
    {
        // Check if the current wave has ended
        if (waveInProgress &&
            GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waveInProgress = false;
            waitingForButton = true;
            Debug.Log("Wave finished. Waiting for player input.");
        }
    }

    /// <summary>
    /// Call this from a UI Button
    /// </summary>
    public void StartNextWave()
    {
        if (waveInProgress || !waitingForButton)
            return;

        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves completed!");
            return;
        }

        waitingForButton = false;
        StartCoroutine(SpawnWave(waves[currentWaveIndex]));
    }

    IEnumerator SpawnWave(Wave wave)
    {
        waveInProgress = true;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefabs);
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }
    }

    void SpawnEnemy(GameObject[] enemyPrefabs)
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedPrefab = enemyPrefabs[randomIndex];

        GameObject enemy = Instantiate(
            selectedPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        EnemyPathAI pathAI = enemy.GetComponent<EnemyPathAI>();
        if (pathAI != null)
        {
            pathAI.SetPath(pathPoints);
        }

        enemy.tag = "Enemy";
    }
}
