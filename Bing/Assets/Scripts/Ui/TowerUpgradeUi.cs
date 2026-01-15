using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TowerUpgradeUI : MonoBehaviour
{
    public static TowerUpgradeUI Instance;

    [Header("UI References")]
    public GameObject panel;
    public TMP_Text upgradeNameText;
    public TMP_Text upgradeDescriptionText;
    public TMP_Text statsText;
    public Button upgradeButton;

    private TowerController selectedTower;
    private bool ignoreNextClick;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    private void Update()
    {
        if (!panel.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            // Ignore the click that opened the menu
            if (ignoreNextClick)
            {
                ignoreNextClick = false;
                return;
            }

            // If clicking NOT on UI → close
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                CloseMenu();
            }
        }
    }

    public void OpenUpgradeMenu(TowerController tower)
    {
        selectedTower = tower;
        panel.SetActive(true);

        // Hide TowerSelectionUI
        if (TowerSelectionUI.Instance != null)
            TowerSelectionUI.Instance.gameObject.SetActive(false);

        RefreshUI();
        ignoreNextClick = true;
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        selectedTower = null;

        // Show TowerSelectionUI again
        if (TowerSelectionUI.Instance != null)
            TowerSelectionUI.Instance.gameObject.SetActive(true);
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
        if (selectedTower == null) return;

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
