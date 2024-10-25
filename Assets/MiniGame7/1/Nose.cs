using UnityEngine;

public class Nose : MonoBehaviour
{
    public bool hit_nose = false;

    private void Update()
    {
        // Update the object's position to follow the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set Z to 0 to keep it in 2D space
        transform.position = mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object collided with something tagged "Nose"
        if (other.CompareTag("Nose"))
        {
            hit_nose = true;
            Debug.Log("Hit the nose!");
        }
    }
}