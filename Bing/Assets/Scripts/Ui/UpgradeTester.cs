using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnUpgradeButtonPressed()
    {
        TowerController.Instance.ApplyUpgrade();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
