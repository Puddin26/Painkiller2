using UnityEngine;
using System;
using System.Collections.Generic;

public class SimplifiedObjectMoveAndSnap : MonoBehaviour
{
    public float snapDistance = 1f;
    public List<Vector2> snapPositions; // List of possible snap positions
    public Vector2 dragRangeMin; // Minimum range for dragging
    public Vector2 dragRangeMax; // Maximum range for dragging

    public GameObject outlineObject;
    
    protected static SimplifiedObjectMoveAndSnap currentlyMovingObject = null;
    protected Camera mainCamera;
    protected bool snapped = false;

    // Static variable to track topmost z-position
    private static float topZPosition = -7f;

    public static event Action<SimplifiedObjectMoveAndSnap> OnObjectSnapped;

    protected virtual void Start()
    {
        mainCamera = Camera.main;
        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        HandleMovement();
    }

    protected virtual void HandleMovement()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Maintain original z for idle state
        
        if (Input.GetMouseButtonDown(0) && !snapped)
        {
            Bounds objectBounds = GetComponent<Renderer>().bounds;
            if (currentlyMovingObject == null && objectBounds.Contains(mousePosition))
            {
                currentlyMovingObject = this;

                // Bring the object to the front by lowering its z-position during dragging
                transform.position = new Vector3(transform.position.x, transform.position.y, topZPosition - 1);
            }
        }

        if (Input.GetMouseButton(0) && currentlyMovingObject == this)
        {
            // Follow the mouse while maintaining the z position to stay on top
            mousePosition.z = topZPosition - 1;

            // Enforce dragging range limits
            mousePosition.x = Mathf.Clamp(mousePosition.x, dragRangeMin.x, dragRangeMax.x);
            mousePosition.y = Mathf.Clamp(mousePosition.y, dragRangeMin.y, dragRangeMax.y);

            transform.position = mousePosition;
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetMouseButtonUp(0) && currentlyMovingObject == this)
        {
            SnapToPosition();
            currentlyMovingObject = null;
            topZPosition -= 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, topZPosition);
        }
    }

    protected virtual void SnapToPosition()
    {
        Vector2 closestPosition = transform.position; // Initialize to current position
        float closestDistance = snapDistance; // Set initial closest distance to snapDistance

        foreach (Vector2 position in snapPositions)
        {
            float distance = Vector2.Distance(transform.position, position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = position;
            }
        }

        if (closestDistance < snapDistance)
        {
            transform.position = new Vector3(closestPosition.x, closestPosition.y, 0f);
            snapped = true;
            OnObjectSnapped?.Invoke(this);
        }
        else
        {
            snapped = false;
        }
    }


    protected virtual void ShowOutline(bool show)
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(show);
            //outlineObject.transform.position = new Vector3(snapPosition.x, snapPosition.y, 0.9f);
        }
    }
}
