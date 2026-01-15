using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    public static TowerSelectionUI Instance;

    [Header("Available Towers")]
    public List<GameObject> towerPrefabs = new List<GameObject>();

    private int selectedIndex = -1;

    private void Awake()
    {
        Instance = this;
    }

    // Called by UI buttons
    public void SelectTower(int index)
    {
        if (index < 0 || index >= towerPrefabs.Count)
        {
            selectedIndex = -1;
            return;
        }

        selectedIndex = index;
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
    }

    public bool HasSelection()
    {
        return selectedIndex != -1;
    }
}
