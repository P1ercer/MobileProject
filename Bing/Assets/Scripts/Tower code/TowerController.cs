using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    //private GameObject prefab;
    public GameObject Sharp;
    public GameObject Explosive;
    public float shootSpeed = 10f;
    public float bulletLifetime = 2.0f;
    public float shootDelay = 0.5f;
    float timer = 0;
    //if isSharp is false, the damage type is explosive. if its true, its sharp
    public bool isSharp;
    public bool isExplosive;
    public float shootTriggerDistance = 5;
    GameObject enemy;

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
            //prefab.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
            //Destroy(prefab, bulletLifetime);
        }
    }
    private void SpawnBullet(Vector3 shootDir)
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
}
