using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float turnStrength = 5f; // higher = stronger curve
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        GameObject target = FindClosestEnemy();
        if (target == null) return;

        Vector2 toTarget = (target.transform.position - transform.position).normalized;

        // Smoothly curve velocity toward target
        rb.velocity = Vector2.Lerp(
            rb.velocity,
            toTarget * rb.velocity.magnitude,
            turnStrength * Time.fixedDeltaTime
        );

        // Rotate to face movement direction
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float shortestDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                closest = enemy;
            }
        }
        return closest;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
