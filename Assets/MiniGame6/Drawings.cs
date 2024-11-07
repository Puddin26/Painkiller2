using System.Collections.Generic;
using UnityEngine;

public class Drawings : MonoBehaviour
{
    public GameObject linePrefab; // Assign a line prefab with a LineRenderer in the Inspector
    private List<GameObject> storedLines = new List<GameObject>(); // Stores each line as a separate GameObject
    private GameObject currentLine;
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartNewLine();
        }
        if (Input.GetMouseButton(0) && currentLine != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Add points only if the mouse has moved a minimum distance
            if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], mousePosition) > 0.1f)
            {
                AddPoint(mousePosition);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            FinalizeLine();
        }
    }

    private void StartNewLine()
    {
        currentLine = Instantiate(linePrefab);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        points.Clear();
        AddPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void AddPoint(Vector3 point)
    {
        points.Add(point);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    private void FinalizeLine()
    {
        if (currentLine != null)
        {
            storedLines.Add(currentLine); // Save the completed line for future use
            currentLine = null;
        }
    }

    public List<GameObject> GetStoredLines()
    {
        return new List<GameObject>(storedLines); // Provide a copy of stored lines to avoid external modification
    }

    public void ClearAllStoredLines()
    {
        foreach (GameObject line in storedLines)
        {
            Destroy(line);
        }
        storedLines.Clear();
    }
}
