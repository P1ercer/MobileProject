using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpshooterTower : MonoBehaviour
{
    public static SharpshooterTower Instance;

    // Projectile
    public GameObject Sharp;

    // Stats
    public int sharpDamage = 1;
    public float shootSpeed = 10f;
    public float bulletLifetime = 2.0f;
    public float shootDelay = 0.5f;
    public float timer = 0;

    // Misc.
    public float shootTriggerDistance = 5f;
    [HideInInspector] public GameObject enemy;

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
        if (enemy == null) return;

        timer += Time.deltaTime;

        Vector3 shootDir = enemy.transform.position - transform.position;

        if (shootDir.magnitude < shootTriggerDistance && timer > shootDelay)
        {
            timer = 0f;
            shootDir.Normalize();
            SpawnBullet(shootDir);
        }
    }

    public void SpawnBullet(Vector3 shootDir)
    {
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject sharpObject = Instantiate(Sharp, transform.position, rotation);
        sharpObject.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
        Destroy(sharpObject, bulletLifetime);
    }
}
