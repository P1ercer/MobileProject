using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TowerUpgradeUI : MonoBehaviour
{
    public static TowerUpgradeUI Instance;

    [Header("UI References")]
    public GameObject panel;          // Your UpgradePanel
    public TMP_Text upgradeNameText;
    public TMP_Text upgradeDescriptionText;
    public TMP_Text statsText;
    public Button upgradeButton;

    private TowerController selectedTower;

    // Track if panel just opened to ignore the click that opened it
    private bool ignoreNextClick = false;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    private void Update()
    {
        if (panel.activeSelf && Input.GetMouseButtonDown(0))
        {
            // Ignore the first click that opened the panel
            if (ignoreNextClick)
            {
                ignoreNextClick = false;
                return;
            }

            // If click is NOT over the panel or any of its children, close it
            if (!IsPointerOverPanel())
            {
                CloseMenu();
            }
        }
    }

    public void OpenUpgradeMenu(TowerController tower)
    {
        selectedTower = tower;
        panel.SetActive(true);
        RefreshUI();

        // Ignore the click that triggered this
        ignoreNextClick = true;
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        selectedTower = null;
    }

    public void UpgradeTower()
    {
        if (selectedTower == null || selectedTower.upgradeApplied)
            return;

        selectedTower.ApplyUpgrade();
        RefreshUI();
    }

    private void RefreshUI()
    {
        upgradeNameText.text = selectedTower.upgradeName;
        upgradeDescriptionText.text = selectedTower.upgradeDescription;

        statsText.text =
            $"Sharp Damage: {selectedTower.sharpDamage}\n" +
            $"Explosive Damage: {selectedTower.explosiveDamage}\n" +
            $"Shoot Speed: {selectedTower.shootSpeed}\n" +
            $"Shoot Delay: {selectedTower.shootDelay}\n" +
            $"Range: {selectedTower.shootTriggerDistance}";

        upgradeButton.interactable = !selectedTower.upgradeApplied;
    }

    // Check if mouse is over the panel or its children
    private bool IsPointerOverPanel()
    {
        if (EventSystem.current == null) return false;

        return RectTransformUtility.RectangleContainsScreenPoint(
            panel.GetComponent<RectTransform>(),
            Input.mousePosition,
            Camera.main
        );
    }
}
