using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour
{
    // Projectile prefabs
    public GameObject Sharp;
    public GameObject Explosive;

    [Header("Tower Info")]
    // Display name of the tower
    public string towerName;

    // Description shown in UI
    [TextArea(3, 6)]
    public string towerDescription;

    [Header("stats")]
    // Damage values for different bullet types
    public int sharpDamage = 1;
    public int explosiveDamage = 10;

    // Bullet movement speed
    public float shootSpeed = 10f;

    // How long bullets exist before being destroyed
    public float bulletLifetime = 2.0f;

    // Time between shots
    public float shootDelay = 0.5f;

    // Internal timer for firing control
    [HideInInspector] public float timer = 0;

    // Determines bullet type: true = sharp, false = explosive
    public bool isSharp;

    // Misc.
    // Whether this tower can fire explosive bullets
    public bool isExplosive;

    // Maximum distance to detect and shoot enemies
    public float shootTriggerDistance = 5;

    [HideInInspector] public GameObject enemy;

    // Stores detected colliders (currently unused)
    private IEnumerable<Collider2D> hits;

    [Header("Upgrade Path")]
    // Upgrade name shown in UI
    public string upgradeName;

    // Upgrade description shown in UI
    [TextArea]
    public string upgradeDescription;

    // Stat increases from upgrading
    public int sharpDamageIncrease;
    public int explosiveDamageIncrease;
    public float shootSpeedIncrease;
    public float shootDelayReduction;
    public float rangeIncrease;

    // Prevents multiple upgrades
    public bool upgradeApplied = false;

    [Header("Economy")]
    // Base cost of the tower
    public int TowerPrice = 50;

    // Cost to upgrade the tower
    public int upgradePrice = 75;

    // Percentage of invested gold refunded when sold
    [Range(0f, 1f)]
    public float sellRefundPercent = 0.5f;

    // Tracks total gold spent on this tower
    private int totalInvestedGold;

    void Start()
    {
        // Find an enemy in the scene
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        // Track initial gold investment
        totalInvestedGold = TowerPrice;
    }

    void Update()
    {
        // Stop processing if no enemy exists
        if (enemy == null) return;

        // Increment fire timer
        timer += Time.deltaTime;

        // Direction from tower to enemy
        Vector3 shootDir = enemy.transform.position - transform.position;

        // Check range and fire delay
        if (shootDir.magnitude < shootTriggerDistance && timer > shootDelay)
        {
            // Reset timer and fire
            timer = 0;
            shootDir.Normalize();
            SpawnBullet(shootDir);
        }
    }

    // Spawns and fires a projectile
    public void SpawnBullet(Vector3 shootDir)
    {
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        // Sharp projectile
        if (isSharp && Sharp != null)
        {
            GameObject sharpObj = Instantiate(Sharp, transform.position, rotation);

            Rigidbody2D rb = sharpObj.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = shootDir * shootSpeed;

            BulletDamage bd = sharpObj.GetComponent<BulletDamage>();
            if (bd != null)
                bd.AddSourceTower(this);

            Destroy(sharpObj, bulletLifetime);
        }

        // Explosive projectile
        if (isExplosive && Explosive != null)
        {
            GameObject explosiveObj = Instantiate(Explosive, transform.position, rotation);

            Rigidbody2D rb = explosiveObj.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = shootDir * shootSpeed;

            BulletDamage bd = explosiveObj.GetComponent<BulletDamage>();
            if (bd != null)
                bd.AddSourceTower(this);

            Destroy(explosiveObj, bulletLifetime);
        }
    }

    // Called when the tower is clicked
    private void OnMouseDown()
    {
        // Ignore clicks when hovering over UI
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        // Ensure UI manager exists
        if (TowerUIManager.Instance == null)
        {
            Debug.LogError("TowerUIManager missing!");
            return;
        }

        // Open upgrade menu for this tower
        Debug.Log("Tower clicked: " + towerName);
        TowerUIManager.Instance.OpenUpgradeMenu(this);
    }

    // Checks if tower is eligible for an upgrade
    public bool CanUpgrade()
    {
        return !upgradeApplied && CurrencyManager.Instance.HasGold(upgradePrice);
    }

    // Applies stat upgrades to the tower
    public void ApplyUpgrade()
    {
        if (upgradeApplied) return;
        if (!CurrencyManager.Instance.SpendGold(upgradePrice)) return;

        // Increase stats
        sharpDamage += sharpDamageIncrease;
        explosiveDamage += explosiveDamageIncrease;
        shootSpeed += shootSpeedIncrease;
        shootDelay = Mathf.Max(0.05f, shootDelay - shootDelayReduction);
        shootTriggerDistance += rangeIncrease;

        // Mark upgrade as applied
        upgradeApplied = true;

        // Track total gold invested
        totalInvestedGold += upgradePrice;
    }

    // Calculates gold returned when selling the tower
    public int GetSellValue()
    {
        return Mathf.RoundToInt(totalInvestedGold * sellRefundPercent);
    }

    // Sells the tower and refunds gold
    public void SellTower()
    {
        CurrencyManager.Instance.AddGold(GetSellValue());
        Destroy(gameObject);
    }
}
