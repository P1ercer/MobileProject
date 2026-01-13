using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public static TowerController Instance;

    //private GameObject prefab;
    public GameObject Sharp;
    public GameObject Explosive;

    // Stats
    public int sharpDamage = 1;
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
    [HideInInspector] public GameObject enemy;
    private IEnumerable<Collider2D> hits;

    void Awake()
    {
        Instance = this;
    }

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
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        if (isSharp)
        {
            GameObject SharpObject = Instantiate(Sharp, transform.position, rotation);
            SharpObject.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
            Destroy(SharpObject, bulletLifetime);
        }

        if (isExplosive)
        {
            GameObject ExplodyObject = Instantiate(Explosive, transform.position, rotation);
            ExplodyObject.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
            Destroy(ExplodyObject, bulletLifetime);
        }
    }

}
