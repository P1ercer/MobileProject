using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTester : MonoBehaviour
{
    public static UpgradeTester Instance;

    private TowerController selectedTower;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false); // Hidden by default
    }

    public void ShowForTower(TowerController tower)
    {
        selectedTower = tower;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        selectedTower = null;
        gameObject.SetActive(false);
    }

    public void OnUpgradeButtonPressed()
    {
        if (selectedTower != null)
        {
            selectedTower.ApplyUpgrade();
        }
    }
}
