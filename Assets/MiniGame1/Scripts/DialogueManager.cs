using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Transform dialogueContainer; // Parent container for speech bubbles
    public Sprite[] bubbleSprites; // Array of bubble sprites
    public bool[] alignLeft; // Array for alignment direction of each bubble (true = left, false = right)

    private int currentBubbleIndex = 0;

    void Start()
    {
        ShowNextBubble();
    }

    public void ShowNextBubble()
    {
        if (currentBubbleIndex < bubbleSprites.Length)
        {
            // Create a new GameObject with a SpriteRenderer for each bubble
            GameObject bubbleObject = new GameObject("Bubble_" + currentBubbleIndex);
            bubbleObject.transform.SetParent(dialogueContainer);

            // Add and configure the SpriteRenderer
            SpriteRenderer spriteRenderer = bubbleObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = bubbleSprites[currentBubbleIndex];

            // Set alignment by adjusting the bubble's position
            AlignBubble(bubbleObject.transform, alignLeft[currentBubbleIndex]);

            currentBubbleIndex++;
        }
    }

    private void AlignBubble(Transform bubbleTransform, bool alignLeft)
    {
        // Set position based on alignment (adjust these values as needed)
        bubbleTransform.localPosition = alignLeft ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Show the next bubble on mouse click
        {
            ShowNextBubble();
        }
    }
}