using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    
        [HideInInspector] public TowerController sourceTower;

        private void Awake()
        {
            sourceTower = GetComponentInParent<TowerController>();
        }
    


    public void Init(TowerController tower)
    {
        sourceTower = tower;
    }

    public int GetDamage()
    {
        if (sourceTower == null) return 0;

        return sourceTower.isSharp
            ? sourceTower.sharpDamage
            : sourceTower.explosiveDamage;
    }


}
