using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBaseDamage : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Base"))
            return;

        if (BaseHealth.Instance != null && enemyHealth != null)
        {
            BaseHealth.Instance.TakeDamage(enemyHealth.health);
        }

        // Enemy is consumed after hitting base
        Destroy(gameObject);
    }
}
