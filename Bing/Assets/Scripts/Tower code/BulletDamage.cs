using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public List<TowerController> sourceTowers = new List<TowerController>();

    public int GetDamage()
    {
        int totalDamage = 0;

        foreach (TowerController tower in sourceTowers)
        {
            if (tower == null) continue;

            if (CompareTag("Sharp"))
                totalDamage += tower.sharpDamage;

            else if (CompareTag("Explosion"))
                totalDamage += tower.explosiveDamage;
        }

        return totalDamage;
    }

    public void AddSourceTower(TowerController tower)
    {
        if (tower != null && !sourceTowers.Contains(tower))
            sourceTowers.Add(tower);
    }

    public void CopySourcesFrom(BulletDamage other)
    {
        if (other == null) return;

        foreach (TowerController tower in other.sourceTowers)
        {
            AddSourceTower(tower);
        }
    }
}
