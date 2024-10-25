using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float pointSpacing = 0.1f; // Space between each point in the line

    private List<Vector3> points = new List<Vector3>();

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Start drawing
        {
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0)) // While holding the mouse button
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            if (points.Count == 0 || Vector3.Distance(mousePosition, points[points.Count - 1]) >= pointSpacing)
            {
                points.Add(mousePosition);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, mousePosition);
            }
        }
    }

    public List<Vector3> GetPoints()
    {
        return points;
    }
}
