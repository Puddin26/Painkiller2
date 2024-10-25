using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public SpriteRenderer bubbleSpriteRenderer; // Assign the SpriteRenderer for the bubble
    public RectTransform rectTransform; // For adjusting position (left or right alignment)

    public void SetBubble(Sprite bubbleSprite, bool alignLeft)
    {
        bubbleSpriteRenderer.sprite = bubbleSprite;
        AlignBubble(alignLeft);
    }

    void AlignBubble(bool left)
    {
        if (left)
        {
            rectTransform.anchorMin = new Vector2(0, 0.5f); // Align to left
            rectTransform.anchorMax = new Vector2(0, 0.5f);
            rectTransform.pivot = new Vector2(0, 0.5f);
        }
        else
        {
            rectTransform.anchorMin = new Vector2(1, 0.5f); // Align to right
            rectTransform.anchorMax = new Vector2(1, 0.5f);
            rectTransform.pivot = new Vector2(1, 0.5f);
        }
    }
}