using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public static TowerController Instance;

    //private GameObject prefab;
    public GameObject Sharp;
    public GameObject Explosive;

    [Header("stats")]
    public int sharpDamage = 1;
    public int explosiveDamage = 10;
    public float shootSpeed = 10f;
    public float bulletLifetime = 2.0f;
    public float shootDelay = 0.5f;
   [HideInInspector] public float timer = 0;
    //if isSharp is false, the damage type is explosive. if its true, its sharp
    public bool isSharp;

    // Misc.
    public bool isExplosive;
    public float shootTriggerDistance = 5;
    [HideInInspector] public GameObject enemy;
    private IEnumerable<Collider2D> hits;

    [Header("Upgrade Path")]
    public string upgradeName;
    [TextArea]
    public string upgradeDescription;

    public int sharpDamageIncrease;
    public int explosiveDamageIncrease;
    public float shootSpeedIncrease;
    public float shootDelayReduction;
    public float rangeIncrease;

    public bool upgradeApplied = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // ApplyUpgrade();
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
            GameObject sharpObj = Instantiate(Sharp, transform.position, rotation);
            sharpObj.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;

            sharpObj.GetComponent<BulletDamage>().damage = sharpDamage;

            Destroy(sharpObj, bulletLifetime);
        }

        if (isExplosive)
        {
            GameObject explosiveObj = Instantiate(Explosive, transform.position, rotation);
            explosiveObj.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;

            explosiveObj.GetComponent<BulletDamage>().damage = explosiveDamage;

            Destroy(explosiveObj, bulletLifetime);
        }
    }


    public void OnMouseDown()
    {
        TowerUpgradeUI.Instance.OpenUpgradeMenu(this);
    }
    //Apply the upgrade
    public void ApplyUpgrade()
    {
        if (upgradeApplied) return;

        // Increase stats by the upgrade values
        sharpDamage += sharpDamageIncrease;
        explosiveDamage += explosiveDamageIncrease;
        shootSpeed += shootSpeedIncrease;
        shootDelay = Mathf.Max(0.05f, shootDelay - shootDelayReduction);
        shootTriggerDistance += rangeIncrease;

        upgradeApplied = true;

        Debug.Log($"Upgrade Applied: {upgradeName} - {upgradeDescription}");
        Debug.Log($"New Stats => SharpDamage: {sharpDamage}, ExplosiveDamage: {explosiveDamage}, ShootSpeed: {shootSpeed}, ShootDelay: {shootDelay}, Range: {shootTriggerDistance}");
    }
}
