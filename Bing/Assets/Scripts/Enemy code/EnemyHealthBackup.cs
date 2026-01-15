using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBackup : MonoBehaviour
{
    public bool IsExoskeleton;
    public int health = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletDamage bullet = collision.GetComponent<BulletDamage>();

        if (bullet == null) return;

        if (collision.CompareTag("Sharp"))
        {
            if (!IsExoskeleton)
            {
                TakeDamage(bullet.damage);
            }
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Explosive"))
        {
            TakeDamage(0);
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
            Destroy(gameObject);
        }
    }
}
