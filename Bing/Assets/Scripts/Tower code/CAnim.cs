using UnityEngine;

public class CAnim : MonoBehaviour
{
    [Header("References")]
    public TowerController tower;   // Reference to the tower
    public Animator animator;       // Reference to the Animator

    [Header("Animation Names")]
    public string shootAnim = "CannoneerShoot";
    public string idleAnim = "CannoneerIdle";

    private void Reset()
    {
        // Auto-assign Animator if not set
        if (animator == null)
            animator = GetComponent<Animator>();

        // Auto-assign TowerController if not set
        if (tower == null)
            tower = GetComponent<TowerController>();
    }

    void Update()
    {
        if (tower == null || animator == null)
            return;

        bool hasEnemy = tower.enemy != null;

        if (hasEnemy)
        {
            // Check distance to enemy
            float distance = Vector3.Distance(transform.position, tower.enemy.transform.position);

            if (distance <= tower.shootTriggerDistance)
            {
                // Enemy in range -> play shooting animation
                animator.Play(shootAnim);
                return;
            }
        }

        // No enemy or out of range -> idle animation
        animator.Play(idleAnim);
    }
}
