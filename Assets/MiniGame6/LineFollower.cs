using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollower : MonoBehaviour
{
    public DrawLine lineDrawer; // Reference to the DrawLine script
    public float speed = 5f;
    private List<Vector3> points;
    private int currentPointIndex = 0;

    void Start()
    {
        points = new List<Vector3>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) // When the player finishes drawing
        {
            points = lineDrawer.GetPoints(); // Corrected the reference to the instance
            currentPointIndex = 0;
            StopAllCoroutines();
            if (points.Count > 0)
            {
                StartCoroutine(FollowLine());
            }
        }
    }

    IEnumerator FollowLine()
    {
        while (currentPointIndex < points.Count)
        {
            Vector3 targetPoint = points[currentPointIndex];
            while (Vector3.Distance(transform.position, targetPoint) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                yield return null;
            }
            currentPointIndex++;
        }
    }
}