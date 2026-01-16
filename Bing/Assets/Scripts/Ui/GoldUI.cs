using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    public TMP_Text goldText;

    void Update()
    {
        goldText.text = $"Gold: {CurrencyManager.Instance.gold}";
    }
}
