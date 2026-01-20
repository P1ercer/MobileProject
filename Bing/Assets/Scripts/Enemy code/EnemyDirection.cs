using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    private EnemyPathAI pathAI;

    void Awake()
    {
        pathAI = GetComponent<EnemyPathAI>();
    }

    void Update()
    {
        if (pathAI == null || pathAI.pathPoints == null || pathAI.pathPoints.Length == 0)
            return;

        int index = pathAI.CurrentIndex;
        if (index >= pathAI.pathPoints.Length)
            return;

        Vector3 direction = pathAI.pathPoints[index].position - transform.position;

        if (direction.sqrMagnitude < 0.0001f)
            return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Top (+Y) faces forward
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
