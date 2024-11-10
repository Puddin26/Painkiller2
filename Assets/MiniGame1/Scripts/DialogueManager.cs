using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public RectTransform dialogueContainer;
    public Sprite[] bubbleSprites;
    public bool[] alignLeft; // Customizable alignment for each bubble
    [Tooltip("Scale of the text bubbles.")]
    [SerializeField] private float bubbleScale = 1f;
    [Tooltip("Vertical distance to move existing bubbles up.")]
    [SerializeField] private float moveUpDistance = 50f;
    [Tooltip("Horizontal distance to align bubbles left or right.")]
    [SerializeField] private float alignDistance = 100f;
    public RectTransform Clickable; // Only clicks within this area will trigger the dialogue

    private int currentBubbleIndex = 0;
    private float cumulativeHeight = 0; // Track the total height of all bubbles

    public Follower follower;

    void Start()
    {
        ShowNextBubble();
    }

    public void ShowNextBubble()
    {
        if (currentBubbleIndex < bubbleSprites.Length)
        {
            // Move existing bubbles up
            foreach (RectTransform child in dialogueContainer)
            {
                child.anchoredPosition += new Vector2(0, moveUpDistance);
            }

            // Create a new UI GameObject with an Image for each bubble
            GameObject bubbleObject = new GameObject("Bubble_" + currentBubbleIndex, typeof(RectTransform), typeof(Image));
            RectTransform bubbleTransform = bubbleObject.GetComponent<RectTransform>();
            bubbleTransform.SetParent(dialogueContainer, false);
            
            // Set the anchors to the bottom
            bubbleTransform.anchorMin = new Vector2(0.5f, 0); // Anchor X in the center, Y at the bottom
            bubbleTransform.anchorMax = new Vector2(0.5f, 0); // Anchor X in the center, Y at the bottom
            bubbleTransform.pivot = new Vector2(0.5f, 0); // Set pivot to the bottom center

            // Configure the Image component
            Image image = bubbleObject.GetComponent<Image>();
            image.sprite = bubbleSprites[currentBubbleIndex];

            // Calculate and set the RectTransform size based on the sprite's native size
            Sprite sprite = image.sprite;
            float width = (sprite.rect.width / sprite.pixelsPerUnit) * bubbleScale;
            float height = (sprite.rect.height / sprite.pixelsPerUnit) * bubbleScale;
            bubbleTransform.sizeDelta = new Vector2(width, height);

            // Set the anchored position of the new bubble to the initial position (e.g., at the bottom)
            bubbleTransform.anchoredPosition = new Vector2(0, 0.5f);

            // Align the bubble to the left or right
            AlignBubble(bubbleTransform, alignLeft[currentBubbleIndex]);

            // Update the cumulative height and the size of the dialogueContainer
            cumulativeHeight += moveUpDistance;
            dialogueContainer.sizeDelta = new Vector2(dialogueContainer.sizeDelta.x, cumulativeHeight + 0.5f);

            currentBubbleIndex++;

            if(currentBubbleIndex == 6 && gameObject.name == "DialogueManager2(Clone)") { follower.letsmove = true; }
        }
    }

    private void AlignBubble(RectTransform bubbleTransform, bool alignLeft)
    {
        // Calculate the X position for alignment
        float spriteHalfWidth = bubbleTransform.sizeDelta.x * 0.5f;

        if (alignLeft)
        {
            // Align based on the left edge
            bubbleTransform.anchoredPosition += new Vector2(-alignDistance + spriteHalfWidth, 0);
        }
        else
        {
            // Align based on the right edge
            bubbleTransform.anchoredPosition += new Vector2(alignDistance - spriteHalfWidth, 0);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Show the next bubble on mouse click
        {
            // Check if the mouse click is within the Clickable RectTransform
            if (RectTransformUtility.RectangleContainsScreenPoint(Clickable, Input.mousePosition, Camera.main))
            {
                ShowNextBubble();
                AudioManager.instance.TarotDialogue();
            }
        }
    }
}
