using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public bool IsExoskeleton;
    public int health = 30;
    private TowerController towerController;



    void Start()
    {
        if (towerController == null)
        {
            towerController = GetComponent<TowerController>();
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When hit by a Tower bullet
        if (collision.gameObject.tag == "Sharp")
        {
            if (IsExoskeleton == true)
            {
                TakeDamage(0);
            }
            if (IsExoskeleton == false)
            {
            TakeDamage(sharpDmg);
            Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "Explosive")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Explosion")
        {
            TakeDamage(ExplosionDmg);
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
        Destroy(gameObject);
    }


}
