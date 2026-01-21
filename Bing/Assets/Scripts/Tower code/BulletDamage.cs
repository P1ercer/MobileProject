using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public TowerController sourceTower;

    public int GetDamage()
    {
        if (CompareTag("Sharp"))
            return sourceTower.sharpDamage;

        if (CompareTag("Explosion"))
            return sourceTower.explosiveDamage;

        return 0;
    }
}
