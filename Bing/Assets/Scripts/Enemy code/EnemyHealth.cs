using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public bool IsExoskeleton;
    public int health = 30;



    void Start()
    {
      
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
            TakeDamage(1);
            Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "Explosive")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Explosion")
        {
            TakeDamage(10);
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
