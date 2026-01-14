using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public static TowerUpgradeUI Instance;

    [Header("UI References")]
    public GameObject panel;
    public Text upgradeNameText;
    public Text upgradeDescriptionText;
    public Text statsText;
    public Button upgradeButton;

    private TowerController selectedTower;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    // Called from TowerController.OnMouseDown()
    public void OpenUpgradeMenu(TowerController tower)
    {
        selectedTower = tower;
        panel.SetActive(true);
        RefreshUI();
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        selectedTower = null;
    }

    public void UpgradeTower()
    {
        if (selectedTower == null)
            return;

        if (selectedTower.upgradeApplied)
            return;

        selectedTower.ApplyUpgrade();
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (selectedTower == null)
            return;

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
}
