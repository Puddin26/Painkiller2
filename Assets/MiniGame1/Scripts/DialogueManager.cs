using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Transform dialogueContainer; 
    public Sprite[] bubbleSprites; 
    public bool[] alignLeft; 
    public float bubbleScale = 1f; 
    
    [SerializeField] private float alignDistance = 1f;
    [SerializeField] private float moveUpDistance = 1f;

    private int currentBubbleIndex = 0;

    void Start()
    {
        ShowNextBubble();
    }

    public void ShowNextBubble()
    {
        if (currentBubbleIndex < bubbleSprites.Length)
        {
            // Move existing bubbles up
            foreach (Transform child in dialogueContainer)
            {
                child.localPosition += new Vector3(0, moveUpDistance, 0);
            }
            
            // Create a new GameObject with a SpriteRenderer for each bubble
            GameObject bubbleObject = new GameObject("Bubble_" + currentBubbleIndex);
            bubbleObject.transform.SetParent(dialogueContainer);

            // Add and configure the SpriteRenderer
            SpriteRenderer spriteRenderer = bubbleObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = bubbleSprites[currentBubbleIndex];

            // Set alignment by adjusting the bubble's position
            AlignBubble(bubbleObject.transform, spriteRenderer, alignLeft[currentBubbleIndex]);

            // Apply the specified uniform scale to the bubble
            bubbleObject.transform.localScale = Vector3.one * bubbleScale;

            currentBubbleIndex++;
        }
    }

    private void AlignBubble(Transform bubbleTransform, SpriteRenderer spriteRenderer, bool alignLeft)
    {
        float spriteHalfWidth = (spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit) * 0.5f * bubbleScale;

        // Set position based on alignment
        if (alignLeft)
        {
            // Align based on the left edge
            bubbleTransform.localPosition = new Vector3(-alignDistance + spriteHalfWidth, 0, 0);
        }
        else
        {
            // Align based on the right edge
            bubbleTransform.localPosition = new Vector3(alignDistance - spriteHalfWidth, 0, 0);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Show the next bubble on mouse click
        {
            ShowNextBubble();
        }
    }
}
