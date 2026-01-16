using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    void Start()
    {
        SpawnEnemy();
    }

    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    [Header("Path To Assign")]
    public Transform[] pathPoints;

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        EnemyPathAI enemyPathAI = enemy.GetComponent<EnemyPathAI>();

        if (enemyPathAI != null)
        {
            enemyPathAI.SetPath(pathPoints);
        }
        else
        {
            Debug.LogWarning("Spawned enemy has no EnemyPathAI component!");
        }
    }
}
