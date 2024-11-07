using UnityEngine;
using System.Collections.Generic;

public class Bubble : MonoBehaviour
{
    public List<Sprite> bubbleSprites; // List of possible bubble sprites
    private BubbleManager bubbleManager; // Reference to the BubbleManager
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Pick a random sprite from the list and set it to the bubble's SpriteRenderer
        if (bubbleSprites.Count > 0)
        {
            spriteRenderer.sprite = bubbleSprites[Random.Range(0, bubbleSprites.Count)];
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            CheckIfClicked(mousePosition);
        }
    }

    private void CheckIfClicked(Vector2 mousePosition)
    {
        // Get the bounds of the bubble sprite to check if the click was within its area
        Bounds bounds = spriteRenderer.bounds;
        
        if (bounds.Contains(mousePosition))
        {
            // Check with BubbleManager if the phone call is active
            if (bubbleManager.IsPhoneCallActive())
            {
                return; // Do nothing if phone call is active
            }

            bubbleManager.RemoveBubble(gameObject); // Destroy the bubble if clicked and no phone call
        }
    }

    // Method to set the BubbleManager reference
    public void SetManager(BubbleManager manager)
    {
        bubbleManager = manager;
    }
}