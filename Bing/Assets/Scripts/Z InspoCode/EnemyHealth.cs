using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when I am hit by a player bullet
        if (collision.gameObject.tag == "PlayerBullet")
        {
            //destroy the bullet
            Destroy(collision.gameObject);
            //reduce my hp
            health--;
            //destroy myself if I get too low in health
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
