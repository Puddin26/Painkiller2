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
        mousePosition.z = transform.position.z; 

        if (Input.GetMouseButtonDown(0) && !snapped)
        {
            Bounds objectBounds = GetComponent<Renderer>().bounds;
            if (currentlyMovingObject == null && objectBounds.Contains(mousePosition))
            {
                currentlyMovingObject = this;
                ShowOutline(true);
            }
        }

        if (Input.GetMouseButton(0) && currentlyMovingObject == this)
        {
            transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && currentlyMovingObject == this)
        {
            SnapToPosition();
            ShowOutline(false);
            currentlyMovingObject = null;
        }
    }

    protected virtual void SnapToPosition()
    {
        if (Vector2.Distance(transform.position, snapPosition) <= snapDistance)
        {
            transform.position = new Vector3(snapPosition.x, snapPosition.y, 0.5f);
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
