using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollower : MonoBehaviour
{
    public PassingNoteManager passingNoteManager;
    public DrawLine lineDrawer; // Reference to the DrawLine script
    public float speed = 5f;
    private List<Vector3> points;
    private int currentPointIndex = 0;
    private bool islining;

    void Start()
    {
        points = new List<Vector3>();
        points = lineDrawer.GetPoints(); // Corrected the reference to the instance
        currentPointIndex = 0;
        StopAllCoroutines();
        if (points.Count > 0)
        {
            StartCoroutine(FollowLine());
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
                print(currentPointIndex);
            }
            currentPointIndex++;
            yield return new WaitForSeconds(0.1f);
        }
        passingNoteManager.AdvanceToNextStage();
    }

    private void Update()
    {
        if (points.Count > 0 && !islining)
        {
            StartCoroutine(FollowLine());
            islining = true;
        }
    }
}