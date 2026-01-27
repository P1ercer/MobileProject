using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TowerUIManager : MonoBehaviour
{
    public static TowerUIManager Instance;

    // ─────────────────────────────
    // TOWER SELECTION (Build UI)
    // ─────────────────────────────
    [Header("Tower Selection")]
    public List<GameObject> towerPrefabs = new List<GameObject>();
    private int selectedIndex = -1;

    // ─────────────────────────────
    // TOWER PLACEMENT
    // ─────────────────────────────
    [Header("Tower Placement")]
    public LayerMask placementLayer;
    public bool requireGround = false;
    public bool clearSelectionAfterPlace = true;
    [Header("Tower Info Panel")]
    public GameObject towerInfoPanel;
    public TMP_Text towerNameText;
    public TMP_Text towerDescriptionText;
    public TMP_Text towerPriceText;


    // ─────────────────────────────
    // TOWER UPGRADE UI
    // ─────────────────────────────
    [Header("Upgrade UI")]
    public GameObject upgradePanel;
    public TMP_Text upgradeNameText;
    public TMP_Text upgradeDescriptionText;
    public TMP_Text statsText;
    public Button upgradeButton;

    // ─────────────────────────────
    // BASE HEALTH UI
    // ─────────────────────────────
    [Header("Base Health UI")]
    public TMP_Text baseHealthText;


    private TowerController selectedTower;
    private bool ignoreNextClick;

    [Header("Economy UI")]
    public Button sellButton;
    public TMP_Text sellValueText;
    public TMP_Text upgradeCostText;


    private void Awake()
    {
        Instance = this;

        upgradePanel.SetActive(false);
        towerInfoPanel.SetActive(false);

        if (BaseHealth.Instance != null)
        {
            UpdateBaseHealthUI(
                BaseHealth.Instance.currentHealth,
                BaseHealth.Instance.maxHealth
            );
        }
    }



    private void Update()
    {
        HandleTowerPlacement();
        HandleTowerSelection();
        HandleUpgradeMenuClose();
        HandleDeselect();
    }



    // ─────────────────────────────
    // SELECTION LOGIC
    // ─────────────────────────────
    public void SelectTower(int index)
    {
        if (index < 0 || index >= towerPrefabs.Count)
        {
            ClearSelection();
            return;
        }

        selectedIndex = index;

        // Get tower data from prefab
        TowerController towerData = towerPrefabs[index].GetComponent<TowerController>();
        if (towerData == null)
            return;

        // Show info panel
        towerInfoPanel.SetActive(true);

        towerNameText.text = towerData.towerName;
        towerDescriptionText.text = towerData.towerDescription;
        towerPriceText.text = $"Build Cost: {towerData.TowerPrice}";

        // Make sure upgrade UI is hidden
        upgradePanel.SetActive(false);
        selectedTower = null;
    }


    public GameObject GetSelectedTowerPrefab()
    {
        if (selectedIndex == -1)
            return null;

        return towerPrefabs[selectedIndex];
    }

    public void ClearSelection()
    {
        selectedIndex = -1;
        towerInfoPanel.SetActive(false);
    }


    public bool HasSelection()
    {
        return selectedIndex != -1;
    }
    private void HandleDeselect()
    {
        if (!towerInfoPanel.activeSelf)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(worldPos);

            // Clicked empty space → deselect
            if (hit == null || hit.GetComponentInParent<TowerController>() == null)
            {
                CloseUpgradeMenu();
            }
        }
    }
    private void HandleTowerSelection()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(worldPos);

        if (hit == null)
            return;

        TowerController tower = hit.GetComponentInParent<TowerController>();

        if (tower != null)
        {
            OpenUpgradeMenu(tower);
        }
    }



    // ─────────────────────────────
    // PLACEMENT LOGIC
    // ─────────────────────────────
    private void HandleTowerPlacement()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(worldPos);

        // Don't place on top of towers
        if (hit != null && hit.GetComponentInParent<TowerController>() != null)
            return;

        if (!HasSelection())
            return;

        GameObject prefab = GetSelectedTowerPrefab();
        TowerController towerData = prefab.GetComponent<TowerController>();

        //  Not enough gold
        if (!CurrencyManager.Instance.HasGold(towerData.TowerPrice))
            return;

        //  Spend gold
        CurrencyManager.Instance.SpendGold(towerData.TowerPrice);

        //  Place tower
        Instantiate(prefab, worldPos, Quaternion.identity);

        if (clearSelectionAfterPlace)
            ClearSelection();
    }



    // ─────────────────────────────
    // UPGRADE MENU LOGIC
    // ─────────────────────────────

    //open menu
    public void OpenUpgradeMenu(TowerController tower)
    {
        selectedTower = tower;

        // ── INFO PANEL ──
        towerInfoPanel.SetActive(true);
        towerNameText.text = tower.towerName;
        towerDescriptionText.text = tower.towerDescription;
        towerPriceText.text = $"Build Cost: {tower.TowerPrice}";

        // ── UPGRADE PANEL ──
        upgradePanel.SetActive(true);
        upgradePanel.transform.SetAsLastSibling();

        RefreshUpgradeUI();
        ignoreNextClick = true;
    }




    private void HandleUpgradeMenuClose()
    {
        if (!upgradePanel.activeSelf)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (ignoreNextClick)
            {
                ignoreNextClick = false;
                return;
            }

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                CloseUpgradeMenu();
            }
        }
    }

    //close menu
    public void CloseUpgradeMenu()
    {
        upgradePanel.SetActive(false);
        towerInfoPanel.SetActive(false);
        selectedTower = null;
    }

    //upgrade tower
    public void UpgradeTower()
    {
        if (selectedTower == null || selectedTower.upgradeApplied)
            return;

        selectedTower.ApplyUpgrade();
        RefreshUpgradeUI();
    }

    //refresh ui after upgrading
    private void RefreshUpgradeUI()
    {
        if (selectedTower == null)
            return;

        towerNameText.text = selectedTower.towerName;
        towerDescriptionText.text = selectedTower.towerDescription;

        statsText.text =
            $"Sharp Damage: {selectedTower.sharpDamage}\n" +
            $"Explosive Damage: {selectedTower.explosiveDamage}\n" +
            $"Shoot Speed: {selectedTower.shootSpeed}\n" +
            $"Shoot Delay: {selectedTower.shootDelay}\n" +
            $"Range: {selectedTower.shootTriggerDistance}";

        upgradeCostText.text = $"Upgrade Cost: {selectedTower.upgradePrice}";
        sellValueText.text = $"Sell: {selectedTower.GetSellValue()}";

        upgradeButton.interactable = selectedTower.CanUpgrade();
    }

    //sell the selected tower
    public void SellSelectedTower()
    {
        if (selectedTower == null)
            return;

        selectedTower.SellTower();
        CloseUpgradeMenu();
    }
    public void UpdateBaseHealthUI(int current, int max)
    {
        if (baseHealthText != null)
            baseHealthText.text = $"Base HP: {current} / {max}";
    }



}
