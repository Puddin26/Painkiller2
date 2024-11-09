using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float pointSpacing = 0.1f; // Space between each point in the line
    public Vector2 startPosition; // Starting position defined in Inspector
    public Vector2 endPosition; // Ending position defined in Inspector
    public float positionTolerance = 0.5f; // Tolerance range for start and end positions
    public GameObject paperPlane;
    
    

    private List<Vector3> points = new List<Vector3>();
    private Camera mainCamera;
    private bool isDrawing = false; // Tracks if drawing is allowed

    void Start()
    {
        paperPlane.SetActive(false);
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
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Start drawing only if the mouse is near the start position
        if (Input.GetMouseButtonDown(0) && Vector2.Distance(mousePosition, startPosition) <= positionTolerance)
        {
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        // Continue drawing if within bounds
        if (Input.GetMouseButton(0) && isDrawing)
        {
            if (points.Count == 0 || Vector3.Distance(mousePosition, points[points.Count - 1]) >= pointSpacing)
            {
                points.Add(mousePosition);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, mousePosition);
            }
        }

        // Stop drawing only if the mouse is near the end position
        if (Input.GetMouseButtonUp(0))
        {
            if (Vector2.Distance(mousePosition, endPosition) <= positionTolerance)
            {
                isDrawing = false;
                paperPlane.SetActive(true);
            }
            else
            {
                // Clear the line if it doesn't end near the end position
                points.Clear();
                lineRenderer.positionCount = 0;
            }
        }
    }

    public List<Vector3> GetPoints()
    {
        Debug.Log(points.Count);
        return points;
    }
}
