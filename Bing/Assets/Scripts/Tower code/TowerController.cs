using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    //private GameObject prefab;
    public GameObject Sharp;
    public GameObject Explosive;

    // Stats
    public int sharpDamage = 5;
    public int explosiveDamage = 10;
    public float shootSpeed = 10f;
    public float bulletLifetime = 2.0f;
    public float shootDelay = 0.5f;
    public float timer = 0;
    //if isSharp is false, the damage type is explosive. if its true, its sharp
    public bool isSharp;

    // Misc.
    public bool isExplosive;
    public float shootTriggerDistance = 5;
    public GameObject enemy;
    private IEnumerable<Collider2D> hits;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
  
    void Update()
    {
        timer += Time.deltaTime;
      
        Vector3 shootDir = enemy.transform.position - transform.position;

        if (shootDir.magnitude < shootTriggerDistance && timer > shootDelay)
        {
        
            timer = 0;
            shootDir.Normalize();
            SpawnBullet(shootDir);
        }
    }
    public void SpawnBullet(Vector3 shootDir)
    {
        if (isSharp == true)
        {
            GameObject SharpObject = Instantiate(Sharp, transform.position, Quaternion.identity); 
            SharpObject.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
            Destroy(SharpObject, bulletLifetime);
        }
        if (isExplosive == true)
        {
            GameObject ExplodyObject = Instantiate(Explosive, transform.position, Quaternion.identity);
            ExplodyObject.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
            Destroy(ExplodyObject, bulletLifetime);
        }
    }

    [HideInInspector] public void ExplosionDmg()
    {
        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosiveDamage);
            }

        }
    }
    [HideInInspector]
    public void SharpDmg()
    {
        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(sharpDamage);
            }

        }
    }
}
