using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool IsExoskeleton;
    public int health = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sharp bullet
        if (collision.CompareTag("Sharp"))
        {
            if (!IsExoskeleton)
            {
                TakeDamage(TowerController.Instance.sharpDamage);
            }

            Destroy(collision.gameObject);
        }

        // Explosive bullet
        if (collision.CompareTag("Explosive"))
        {
            TakeDamage(TowerController.Instance.explosiveDamage);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Explosion"))
        {
            TakeDamage(TowerController.Instance.explosiveDamage);
        }
    }

    void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
