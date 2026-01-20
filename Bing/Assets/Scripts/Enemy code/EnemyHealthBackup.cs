using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Needed for Action

public class EnemyHealthBackup: MonoBehaviour
{
    public bool IsExoskeleton;
    public int health = 30;
    public int goldReward = 10;

    public Action onDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletDamage bullet = collision.GetComponent<BulletDamage>();
        if (bullet == null) return;

        int damage = bullet.GetDamage();

        // Sharp damage logic
        if (collision.CompareTag("Sharp"))
        {
            if (!IsExoskeleton)
            {
                TakeDamage(damage);
            }

            Destroy(collision.gameObject);
            return;
        }

        // explosion projectile (spawns explosion)
        if (collision.CompareTag("Explosive"))
        {
            Destroy(collision.gameObject);
            return;
        }

        // explosion damage
        if (collision.CompareTag("Explosion"))
        {
            TakeDamage(damage);
            return;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddGold(goldReward);
        }

        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
