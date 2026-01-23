using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    private Transform target;
    private TowerController towerController;

    void Start()
    {
        towerController = GetComponent<TowerController>();
    }

    void Update()
    {
        FindTarget();
        towerController.enemy = target ? target.gameObject : null;
    }

    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            towerController.shootTriggerDistance
        );

        EnemyPathAI bestEnemy = null;
        float bestProgress = float.MinValue;

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            EnemyPathAI enemyAI = hit.GetComponent<EnemyPathAI>();
            if (enemyAI == null) continue;

            float progress = CalculateProgress(enemyAI);

            if (progress > bestProgress)
            {
                bestProgress = progress;
                bestEnemy = enemyAI;
            }
        }

        target = bestEnemy ? bestEnemy.transform : null;
    }

    float CalculateProgress(EnemyPathAI enemy)
    {
        int index = enemy.CurrentIndex;
        float segmentProgress = 0f;

        if (enemy.pathPoints != null &&
            index < enemy.pathPoints.Length)
        {
            float dist = Vector3.Distance(
                enemy.transform.position,
                enemy.pathPoints[index].position
            );

            segmentProgress = 1f / (1f + dist);
        }

        return index * 1000f + segmentProgress;
    }
}
