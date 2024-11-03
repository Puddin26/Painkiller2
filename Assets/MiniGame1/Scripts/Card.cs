using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite frontSprite;    // The sprite shown after clicking
    public GameObject conversationBoxPrefab;  // Prefab for the conversation box
    public float conversationYOffset = 1.0f;  // Adjustable Y offset for conversation box

    private SpriteRenderer spriteRenderer;
    private bool isClickable = false;  // Controls whether the card can be clicked
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Check if there's a BoxCollider2D and add one if not
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        // Automatically adjust collider size to match the sprite size
        AdjustColliderSize();
    }

    void OnMouseDown()
    {
        if (isClickable)
        {
            spriteRenderer.sprite = frontSprite;  // Change the sprite to the front sprite
            FindObjectOfType<CardManager>().CardClicked(gameObject);  // Notify CardManager
        }
    }

    public GameObject ShowConversation()
    {
        // Instantiate the conversation box prefab
        GameObject conversationBox = Instantiate(conversationBoxPrefab, transform.position + new Vector3(0, -conversationYOffset, 0), Quaternion.identity);
        conversationBox.SetActive(true);  // Ensure the conversation box is activated
        return conversationBox;  // Return the conversation box reference
    }

    public void SetClickable(bool clickable)
    {
        isClickable = clickable;
    }

    private void AdjustColliderSize()
    {
        if (spriteRenderer != null && boxCollider != null)
        {
            boxCollider.size = spriteRenderer.bounds.size;
        }
    }
}