using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public RectTransform dialogueContainer;
    public Sprite[] bubbleSprites;
    public bool[] alignLeft;
    [Tooltip("Scale of the text bubbles.")]
    [SerializeField] private float bubbleScale = 1f;
    [Tooltip("Vertical distance to move existing bubbles up.")]
    [SerializeField] private float moveUpDistance = 50f;
    [Tooltip("Horizontal distance to align bubbles left or right.")]
    [SerializeField] private float alignDistance = 100f;

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
            foreach (RectTransform child in dialogueContainer)
            {
                child.anchoredPosition += new Vector2(0, moveUpDistance);
            }

            // Create a new UI GameObject with an Image for each bubble
            GameObject bubbleObject = new GameObject("Bubble_" + currentBubbleIndex, typeof(RectTransform), typeof(Image));
            RectTransform bubbleTransform = bubbleObject.GetComponent<RectTransform>();
            bubbleTransform.SetParent(dialogueContainer, false); // Use false to maintain local position settings

            // Configure the Image component
            Image image = bubbleObject.GetComponent<Image>();
            image.sprite = bubbleSprites[currentBubbleIndex];

            // Calculate and set the RectTransform size based on the sprite's native size
            Sprite sprite = image.sprite;
            float width = (sprite.rect.width / sprite.pixelsPerUnit) * bubbleScale;
            float height = (sprite.rect.height / sprite.pixelsPerUnit) * bubbleScale;
            bubbleTransform.sizeDelta = new Vector2(width, height);

            // Set initial position and alignment
            AlignBubble(bubbleTransform, alignLeft[currentBubbleIndex]);

            currentBubbleIndex++;
        }
    }

    private void AlignBubble(RectTransform bubbleTransform, bool alignLeft)
    {
        // Calculate the X position for alignment
        float xPosition = alignLeft ? -alignDistance : alignDistance;

        // Set the anchored position to handle alignment
        bubbleTransform.anchoredPosition = new Vector2(xPosition, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Show the next bubble on mouse click
        {
            ShowNextBubble();
        }
    }
}
