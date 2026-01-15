using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTester : MonoBehaviour
{
    public static UpgradeTester Instance;

    [Header("UI")]
    public GameObject panel;
    public Text titleText;
    public Text descriptionText;
    public Text statsText;
    public Button upgradeButton;

    private TowerController currentTower;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowForTower(TowerController tower)
    {
        currentTower = tower;
        panel.SetActive(true);
        RefreshUI();
    }

    public void Close()
    {
        panel.SetActive(false);
        currentTower = null;
    }

    public void ApplyUpgrade()
    {
        if (currentTower == null || currentTower.upgradeApplied)
            return;

        currentTower.ApplyUpgrade();
        RefreshUI();
    }

    void RefreshUI()
    {
        titleText.text = currentTower.upgradeName;
        descriptionText.text = currentTower.upgradeDescription;

        statsText.text =
            $"Sharp Damage: {currentTower.sharpDamage}\n" +
            $"Explosive Damage: {currentTower.explosiveDamage}\n" +
            $"Fire Rate: {currentTower.shootDelay}\n" +
            $"Range: {currentTower.shootTriggerDistance}";

        upgradeButton.interactable = !currentTower.upgradeApplied;
    }
}
