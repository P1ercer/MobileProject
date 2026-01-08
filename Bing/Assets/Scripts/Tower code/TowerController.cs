using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private GameObject prefab;
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
            prefab.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
            Destroy(prefab, bulletLifetime);
        }
    }
    private void SpawnBullet()
    {
        if (isSharp == true)
        {
            GameObject SharpObject = Instantiate(prefab, transform.position, Quaternion.identity);
        }
        if (isExplosive == true)
        {
            GameObject ExplodyObject = Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
