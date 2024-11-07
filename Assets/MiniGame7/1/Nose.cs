using UnityEngine;

public class Nose : MonoBehaviour
{
    public bool hit_nose = false;
    public SpriteRenderer targetObject; // The object to change sprite for, set in the Inspector
    public Sprite newSprite; // The new sprite to change to, set in the Inspector
    public Sprite noseHitSprite; // The new sprite for the "Nose" object upon collision

    private bool isDragging = false;
    private bool isFirstClick = false;
    private SpriteRenderer spriteRenderer; // Reference to the object's own SpriteRenderer

    private void Start()
    {
        // Get the SpriteRenderer and make the object initially invisible
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        // Check if the mouse is clicked and held
        if (Input.GetMouseButtonDown(0))
        {
            if (!isFirstClick)
            {
                // Make the object visible on first click
                spriteRenderer.enabled = true;
                isFirstClick = true;

                // Change the target object's sprite
                if (targetObject != null && newSprite != null)
                {
                    targetObject.sprite = newSprite;
                }
            }
            
            // Start dragging
            isDragging = true;
        }

        // Stop dragging when mouse is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Update the object's position to follow the mouse while dragging
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Set Z to 0 to keep it in 2D space
            transform.position = mousePosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object collided with something tagged "Nose"
        if (other.CompareTag("Nose"))
        {
            hit_nose = true;
            Debug.Log("Hit the nose!");

            // Change the nose object's sprite to the noseHitSprite
            SpriteRenderer noseSpriteRenderer = other.GetComponent<SpriteRenderer>();
            if (noseSpriteRenderer != null && noseHitSprite != null)
            {
                noseSpriteRenderer.sprite = noseHitSprite;
            }
        }
    }
}
