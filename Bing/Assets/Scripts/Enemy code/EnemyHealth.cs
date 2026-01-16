using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Needed for Action

public class EnemyHealth : MonoBehaviour
{
    public bool IsExoskeleton;
    public int health = 30;

    // Added event for wave tracking
    public Action onDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletDamage bullet = collision.GetComponent<BulletDamage>();

        if (bullet == null) return;

        if (collision.CompareTag("Sharp"))
        {
            if (!IsExoskeleton)
            {
                TakeDamage(0);
            }
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Explosive"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Explosion"))
        {
            TakeDamage(bullet.damage);
        }
    }

    void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    // New method to trigger death event
    void Die()
    {
        onDeath?.Invoke(); // Notify the spawner
        Destroy(gameObject); // Then destroy enemy
    }
}
