using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kaboom 
public class ExplosionObj : MonoBehaviour
{
    public GameObject Explosion;
    public float bulletLifetime = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        // Spawn explosion
        GameObject explosionObj = Instantiate(
            Explosion,
            transform.position,
            Quaternion.identity
        );

        BulletDamage projectileBD = GetComponent<BulletDamage>();
        BulletDamage explosionBD = explosionObj.GetComponent<BulletDamage>();

        if (projectileBD != null && explosionBD != null)
        {
            explosionBD.CopySourcesFrom(projectileBD);
        }

        Destroy(explosionObj, bulletLifetime);
        Destroy(gameObject);
    }
}
