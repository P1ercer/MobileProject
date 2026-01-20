using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//capitalism
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

    //gold you have
    public bool HasGold(int amount)
    {
        return gold >= amount;
    }

    //spend gold
    public bool SpendGold(int amount)
    {
        if (!HasGold(amount))
            return false;

        gold -= amount;
        return true;
    }

    //add gp;d
    public void AddGold(int amount)
    {
        gold += amount;
    }
}
