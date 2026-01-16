using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour
{


    //private GameObject prefab;
    public GameObject Sharp;
    public GameObject Explosive;

    [Header("Tower Info")]
    public string towerName;

    [TextArea(3, 6)]
    public string towerDescription;

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

    [Header("Economy")]
    public int TowerPrice = 50;
    public int upgradePrice = 75;
    [Range(0f, 1f)]
    public float sellRefundPercent = 0.5f;

    private int totalInvestedGold;



    void Start()
    {
        // ApplyUpgrade();
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        // Track gold invested in this tower
        totalInvestedGold = TowerPrice;
    }
  
    void Update()
    {
        if (enemy == null) return;

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

        }
    }


    private void OnMouseDown()
    {
        // Ignore clicks over UI
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        if (TowerUIManager.Instance == null)
        {
            Debug.LogError("TowerUIManager missing!");
            return;
        }

        Debug.Log("Tower clicked: " + towerName);
        TowerUIManager.Instance.OpenUpgradeMenu(this);
    }



    public bool CanUpgrade()
    {
        return !upgradeApplied && CurrencyManager.Instance.HasGold(upgradePrice);
    }

    //Apply the upgrade
    public void ApplyUpgrade()
    {
        if (upgradeApplied) return;
        if (!CurrencyManager.Instance.SpendGold(upgradePrice)) return;

        sharpDamage += sharpDamageIncrease;
        explosiveDamage += explosiveDamageIncrease;
        shootSpeed += shootSpeedIncrease;
        shootDelay = Mathf.Max(0.05f, shootDelay - shootDelayReduction);
        shootTriggerDistance += rangeIncrease;

        upgradeApplied = true;
        totalInvestedGold += upgradePrice;
    }

    public int GetSellValue()
    {
        return Mathf.RoundToInt(totalInvestedGold * sellRefundPercent);
    }

    public void SellTower()
    {
        CurrencyManager.Instance.AddGold(GetSellValue());
        Destroy(gameObject);
    }

}
