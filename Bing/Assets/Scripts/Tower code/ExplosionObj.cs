using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionObj : MonoBehaviour
{
    public GameObject Explosion;
    public float bulletLifetime = 10.0f;


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
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject ExplosionObject = Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(ExplosionObject, bulletLifetime);

        }

    }
}
