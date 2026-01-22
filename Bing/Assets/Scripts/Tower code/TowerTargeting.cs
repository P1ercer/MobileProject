using System.Collections;
using System.Collections.Generic;
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

        if (target == null) return;

        //[REMOVED]: Fire control is already handled by TowerController
        towerController.enemy = target.gameObject;
    }

    //find the first enemy in line
    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            towerController.shootTriggerDistance,
            LayerMask.GetMask("Enemy")
            );

        EnemyPathAI bestEnemy = null;
        float bestProgressScore = float.MinValue;

        foreach (Collider2D hit in hits)
        {
            EnemyPathAI enemyAI = hit.GetComponent<EnemyPathAI>();
            if (enemyAI == null) continue;

            float progressScore = CalculateProgress(enemyAI);

            if (progressScore > bestProgressScore)
            {
                bestProgressScore = progressScore;
                bestEnemy = enemyAI;
            }
        }

        if (bestEnemy != null)
        {
            target = bestEnemy.transform;
            towerController.enemy = target.gameObject;
        }
        else
        {
            target = null;
            towerController.enemy = null;
        }
    }
    /// <summary>
    /// Higher value = further along the path
    /// </summary>
    float CalculateProgress(EnemyPathAI enemy)
    {
        int index = enemy.CurrentIndex;

        float distanceBonus = 0f;

        if (enemy.pathPoints != null && index < enemy.pathPoints.Length)
        {
            distanceBonus = 1f - Vector3.Distance(
                enemy.transform.position,
                enemy.pathPoints[index].position
            );
        }

        return index * 1000f + distanceBonus;
    }

    private void OnDrawGizmosSelected()
    {
        if (towerController == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, towerController.shootTriggerDistance);
    }
}
