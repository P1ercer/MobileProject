using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    float timer;
    Transform target;
    public TowerController towerController;

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
    void Update(Vector3 shootDir)
    {
        FindTarget();

        if (target == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            towerController.SpawnBullet(shootDir);
            timer = 1f / timer;
        }
    }
    void FindTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, towerController.shootTriggerDistance, LayerMask.GetMask("Enemy"));
        target = hit ? hit.transform : null;
    }

}
