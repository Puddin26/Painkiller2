using UnityEngine;

public class Nose : MonoBehaviour
{
    public bool hit_nose = false;
    public SpriteRenderer targetObject; // The object to change sprite for, set in the Inspector
    public Sprite newSprite; // The sprite to change to on first click, set in the Inspector
    public Sprite noseHitSprite; // The sprite to change to on "Nose" hit
    public Sprite secondSprite; // The sprite to change to after 1.5 seconds
    public GameObject clickableObject; // The object to check for mouse clicks, set in the Inspector
    public Vector2 spriteOffset = Vector2.zero; // Customizable offset for targetObject's position
    
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
        // Check if the mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over the specified clickable object
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == clickableObject)
            {
                if (!isFirstClick)
                {
                    // Make the object visible on first click
                    spriteRenderer.enabled = true;
                    isFirstClick = true;

                    // Change the target object's sprite and apply the offset
                    if (targetObject != null && newSprite != null)
                    {
                        targetObject.sprite = newSprite;
                        targetObject.transform.position += (Vector3)spriteOffset; // Apply the 2D offset
                    }
                }
            }
        }

        if (isFirstClick)
        {
            Vector3 mouseDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDragPosition.z = 0f; // Set Z to 0 to keep it in 2D space
            transform.position = mouseDragPosition;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hit_nose)
        {
            if (other.CompareTag("Nose"))
            {
                hit_nose = true;
                Debug.Log("Hit the nose!");

                // Change the nose object's sprite to the noseHitSprite
                SpriteRenderer noseSpriteRenderer = other.GetComponent<SpriteRenderer>();
                if (noseSpriteRenderer != null && noseHitSprite != null)
                {
                    noseSpriteRenderer.sprite = noseHitSprite;

                    // Change to secondSprite after 1.5 seconds
                    Invoke("ChangeToSecondSprite", 1.5f);
                }
            }
        }
    }

    private void ChangeToSecondSprite()
    {
        // Change the nose object's sprite to the second sprite after 1.5 seconds
        if (secondSprite != null)
        {
            SpriteRenderer noseSpriteRenderer = GameObject.FindWithTag("Nose").GetComponent<SpriteRenderer>();
            if (noseSpriteRenderer != null)
            {
                noseSpriteRenderer.sprite = secondSprite;
            }
        }
    }
}
