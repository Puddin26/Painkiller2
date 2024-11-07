using UnityEngine;
using System;

public class SimplifiedObjectMoveAndSnap : MonoBehaviour
{
    public float snapDistance = 1f;
    public Vector2 snapPosition;
    public GameObject outlineObject;
    
    protected static SimplifiedObjectMoveAndSnap currentlyMovingObject = null;
    protected Camera mainCamera;
    protected bool snapped = false;

    // Static variable to track topmost z-position
    private static float topZPosition = -7f; // Start with -1, you can adjust this value as needed

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
                transform.position = new Vector3(transform.position.x, transform.position.y, topZPosition-1);
            }
        }

        if (Input.GetMouseButton(0) && currentlyMovingObject == this)
        {
            // Follow the mouse while maintaining the z position to stay on top
            mousePosition.z = topZPosition-1;
            transform.position = mousePosition;
            
            // Reset rotation to zero
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetMouseButtonUp(0) && currentlyMovingObject == this)
        {
            SnapToPosition();

            currentlyMovingObject = null;
            // Set the object to the topmost z-position after drop
            topZPosition -= 1f;  // Decrease the z-position to bring it on top (further forward)
            transform.position = new Vector3(transform.position.x, transform.position.y, topZPosition);
        }
    }

    protected virtual void SnapToPosition()
    {
        if (Vector2.Distance(transform.position, snapPosition) <= snapDistance)
        {
            transform.position = new Vector3(snapPosition.x, snapPosition.y, 0f);
            snapped = true;
            OnObjectSnapped?.Invoke(this);
        }
    }

    protected virtual void ShowOutline(bool show)
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(show);
            outlineObject.transform.position = new Vector3(snapPosition.x, snapPosition.y, 0.9f);
        }
    }
}

