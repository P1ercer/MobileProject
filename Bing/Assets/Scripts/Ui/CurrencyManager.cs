using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int gold = 200;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool HasGold(int amount)
    {
        return gold >= amount;
    }

    public bool SpendGold(int amount)
    {
        if (!HasGold(amount))
            return false;

        gold -= amount;
        return true;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }
}
