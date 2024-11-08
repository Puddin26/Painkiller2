using System.Collections.Generic;
using UnityEngine;

public class Drawings : MonoBehaviour
{
    public LineRenderer lineRenderer; // Directly assign a LineRenderer component in the Inspector
    public GameObject clickableObject; // The invisible object that activates after the first line is drawn
    public float drawingRange = 5f; // Customizable range within which the player can draw, set in Inspector
    public PassingNoteManager passingNoteManager; // Reference to manage stage progression

    private List<Vector3> points = new List<Vector3>();
    private List<List<Vector3>> storedDrawings = new List<List<Vector3>>(); // Stores each drawing as a list of points
    private bool firstLineDrawn = false; // Tracks if the first line has been drawn

    private void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        
        if (clickableObject != null)
        {
            clickableObject.SetActive(false); // Ensure the object is initially invisible
        }

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0; // Start with no points in the LineRenderer
        }
        
        
    }

    private void Update()
    {
        // Start drawing if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            StartNewLine();
        }

        // Continue drawing while holding down the left mouse button
        if (Input.GetMouseButton(0) && lineRenderer != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Restrict drawing within the specified range
            if (Vector3.Distance(mousePosition, transform.position) <= drawingRange)
            {
                // Add points only if the mouse has moved a minimum distance
                if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], mousePosition) > 0.1f)
                {
                    AddPoint(mousePosition);
                }

                // Activate clickableObject when the first line is drawn
                if (!firstLineDrawn && clickableObject != null)
                {
                    clickableObject.SetActive(true);
                    firstLineDrawn = true;
                }
            }
        }

        // Finalize the line when the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            FinalizeLine();
        }

        // Check if the clickable object is clicked to store drawings and move to stage 3
        if (firstLineDrawn && clickableObject != null && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (Vector3.Distance(mousePosition, clickableObject.transform.position) < 0.5f) // Click detection radius
            {
                StoreDrawings();
                passingNoteManager.AdvanceToNextStage(); // Move to stage 3
            }
        }
    }

    private void StartNewLine()
    {
        points.Clear(); // Clear previous points
        lineRenderer.positionCount = 0; // Reset the LineRenderer to start fresh
        AddPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // Add the starting point
    }

    private void AddPoint(Vector3 point)
    {
        points.Add(point);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    private void FinalizeLine()
    {
        // Store the completed line as a separate drawing
        if (points.Count > 0)
        {
            storedDrawings.Add(new List<Vector3>(points)); // Store a copy of the current drawing points
            points.Clear(); // Reset points for the next line
        }
    }

    private void StoreDrawings()
    {
        // Logic for handling or saving storedDrawings as needed
        Debug.Log("All drawings have been stored.");
    }
}
