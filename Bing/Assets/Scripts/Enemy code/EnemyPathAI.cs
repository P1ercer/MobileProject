using UnityEngine;

public class EnemyPathAI : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] pathPoints;
    public float moveSpeed = 2f;

    private int currentPointIndex = 0;

    void Update()
    {
        if (pathPoints == null || pathPoints.Length == 0) return;
        MoveAlongPath();
    }

    public void SetPath(Transform[] newPath)
    {
        pathPoints = newPath;
        currentPointIndex = 0;
    }

    void MoveAlongPath()
    {
        Transform targetPoint = pathPoints[currentPointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            currentPointIndex++;

            if (currentPointIndex >= pathPoints.Length)
            {
                OnReachEnd();
            }
        }
    }

    void OnReachEnd()
    {
        Destroy(gameObject);
    }
}
