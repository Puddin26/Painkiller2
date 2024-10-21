using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite frontSprite;    // The sprite shown after clicking
    public Sprite backSprite;     // The initial sprite
    public GameObject conversationBoxPrefab;  // Prefab for the conversation box
    public float conversationYOffset = 1.0f;  // Adjustable Y offset for conversation box

    private SpriteRenderer spriteRenderer;
    private bool isClicked = false;
    private bool isClickable = false;  // Controls whether the card can be clicked
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = backSprite;

        // Check if there's a BoxCollider2D and add one if not
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        // Automatically adjust collider size to match the sprite size
        AdjustColliderSize();
    }

    void AdjustColliderSize()
    {
        if (spriteRenderer != null && boxCollider != null)
        {
            boxCollider.size = spriteRenderer.size;  // Set the collider size to match the sprite size
        }
    }

    public void SetClickable(bool clickable)
    {
        isClickable = clickable;
    }

    void OnMouseDown()
    {
        // Only allow interaction if the card is marked as clickable and has not been clicked yet
        if (!isClicked && isClickable)
        {
            isClicked = true;
            spriteRenderer.sprite = frontSprite;
            GenerateConversationBox();

            // Notify the CardManager that this card was clicked
            FindObjectOfType<CardManager>().CardClicked(this.gameObject);
        }
    }


    private void GenerateConversationBox()
    {
        // Instantiate the conversation box beneath the card
        Vector3 conversationPosition = new Vector3(transform.position.x, transform.position.y - conversationYOffset, transform.position.z);
        GameObject conversationBox = Instantiate(conversationBoxPrefab, conversationPosition, Quaternion.identity);
    
        // Ensure the instantiated object is active
        conversationBox.SetActive(true);
        
    }

}
