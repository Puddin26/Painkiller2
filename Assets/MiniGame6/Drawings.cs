using System.Collections.Generic;
using UnityEngine;

public class Drawings : MonoBehaviour
{
    public LineRenderer lineRendererPrefab; // Assign a LineRenderer prefab in the Inspector
    public GameObject clickableObject; // The invisible object that activates after the first line is drawn
    public Vector2 minDrawPosition; // Minimum position for drawing range (set in Inspector)
    public Vector2 maxDrawPosition; // Maximum position for drawing range (set in Inspector)
    public PassingNoteManager passingNoteManager; // Reference to manage stage progression

    private List<LineRenderer> lines = new List<LineRenderer>(); // Stores each line's LineRenderer component
    private List<List<Vector3>> storedDrawings = new List<List<Vector3>>(); // Stores each line's points for later access
    private LineRenderer currentLineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private bool firstLineDrawn = false; // Tracks if the first line has been drawn

    private void Start()
    {
        if (clickableObject != null)
        {
            clickableObject.SetActive(false); // Ensure the object is initially invisible
        }
    }

    private void Update()
    {
        // Start drawing a new line if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            StartNewLine();
        }

        // Continue drawing while holding down the left mouse button
        if (Input.GetMouseButton(0) && currentLineRenderer != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Restrict drawing within the specified square area
            if (mousePosition.x >= minDrawPosition.x && mousePosition.x <= maxDrawPosition.x &&
                mousePosition.y >= minDrawPosition.y && mousePosition.y <= maxDrawPosition.y)
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
        GameObject lineObj = Instantiate(lineRendererPrefab.gameObject); // Create a new LineRenderer instance
        currentLineRenderer = lineObj.GetComponent<LineRenderer>();
        lines.Add(currentLineRenderer); // Store the new LineRenderer in the list
        points.Clear(); // Clear points for the new line
        AddPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // Add the starting point
    }

    private void AddPoint(Vector3 point)
    {
        points.Add(point);
        currentLineRenderer.positionCount = points.Count;
        currentLineRenderer.SetPositions(points.ToArray());
    }

    private void FinalizeLine()
    {
        // Add points to current line only for visual representation
        if (currentLineRenderer != null && points.Count > 0)
        {
            points.Clear(); // Reset points for the next line
            currentLineRenderer = null; // Reset current line renderer
        }
    }

    private void StoreDrawings()
    {
        // Populate storedDrawings once, containing all lines' points
        storedDrawings.Clear(); // Ensure no previous entries remain

        foreach (var line in lines)
        {
            List<Vector3> linePoints = new List<Vector3>();
            for (int i = 0; i < line.positionCount; i++)
            {
                linePoints.Add(line.GetPosition(i));
            }
            storedDrawings.Add(linePoints); // Add each line’s points to storedDrawings
        }

        Debug.Log("All drawings have been stored. Number of drawings: " + storedDrawings.Count);
    }

    // Public method to retrieve stored drawings
    public List<List<Vector3>> GetStoredDrawings()
    {
        return storedDrawings;
    }
}
