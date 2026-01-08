using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathAI : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] pathPoints;
    public float moveSpeed = 2f;
    private int currentPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pathPoints.Length == 0) return;
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        Transform targetPoint = pathPoints[currentPointIndex];
        //move towards the target point
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );
        //check if we've reached the point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            currentPointIndex++;
            //Reached end of path
            if(currentPointIndex >= pathPoints.Length)
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
