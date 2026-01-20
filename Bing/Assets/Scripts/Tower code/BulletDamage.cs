using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public TowerController sourceTower;

    //Damage lol
    public int GetDamage()
    {
        if (sourceTower == null) return 0;

        return sourceTower.isSharp
            ? sourceTower.sharpDamage
            : sourceTower.explosiveDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        int damage = GetDamage();

        Destroy(gameObject);
    }
}
