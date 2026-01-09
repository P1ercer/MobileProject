using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject prefab;
    public float shootSpeed = 10f;
    public float bulletLifetime = 2.0f;
    public float shootDelay = 0.5f;
    float timer = 0;
    //shoot at the player if close enough
    public float shootTriggerDistance = 5;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //calculate the vector towards the player, destination - start
        Vector3 shootDir = player.transform.position - transform.position;
        //see if the player is close enough to shoot AND if we have waited long enough to shoot
        if (shootDir.magnitude < shootTriggerDistance && timer > shootDelay)
        {
            //shoot towards the player
            timer = 0;
            shootDir.Normalize();
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * shootSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
}
