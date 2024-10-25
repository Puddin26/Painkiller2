using UnityEngine;

public class Whiskin : MonoBehaviour
{
    public float speedThreshold = 5f;  // Set this to adjust what is considered "fast"
    private Vector3 lastMousePosition;
    private float mouseSpeed;

    private void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        // Follow mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // Keep it in 2D space
        transform.position = mousePosition;

        // Calculate mouse speed
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        mouseSpeed = mouseDelta.x / Time.deltaTime;  // Speed of horizontal movement

        // Check speed and log debug messages
        if (Mathf.Abs(mouseSpeed) < speedThreshold)
        {
            Debug.Log("Too slow");
        }
        else if (Mathf.Abs(mouseSpeed) > speedThreshold)
        {
            Debug.Log("Too fast");
        }

        // Store current mouse position for the next frame
        lastMousePosition = Input.mousePosition;
    }
}