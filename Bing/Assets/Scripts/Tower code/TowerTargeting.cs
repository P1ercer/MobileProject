using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    float timer;
    Transform target;
    private TowerController towerController;

    // Start is called before the first frame update
    void Start()
    {
        if (towerController == null)
        {
            towerController = GetComponent<TowerController>();
        }

        timer = 1f / towerController.timer;

    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();

        if (target == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Vector3 shootDir = target.position - transform.position;
            towerController.SpawnBullet(shootDir);
            timer = 1f; // reset cooldown
        }
    }


    void FindTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            towerController.shootTriggerDistance,
            LayerMask.GetMask("Enemy")
        );

        if (hit != null)
        {
            target = hit.transform;
            towerController.enemy = target.gameObject;
        }
        else
        {
            target = null;
            towerController.enemy = null;
        }
    }

}
