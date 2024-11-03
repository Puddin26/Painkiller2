using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards;    // Array to hold card objects
    public float spacing = 2.0f;  // Horizontal spacing between cards
    public float cardYPosition = 0.0f;  // Adjustable Y position for cards

    private int currentIndex = 0;
    private GameObject currentConversationWindow = null;  // Reference to the currently open conversation window

    void Start()
    {
        ArrangeCards();
        SetCardClickable(currentIndex);  // Enable only the first card at the start
    }

    void ArrangeCards()
    {
        // Calculate the x positions for each card based on the spacing
        for (int i = 0; i < cards.Length; i++)
        {
            float xPosition = (i - 1) * spacing; // Center the middle card at x=0
            cards[i].transform.position = new Vector3(xPosition, cardYPosition, 0);
        }
    }

    public void CardClicked(GameObject card)
    {
        // Close the current conversation window if it exists
        if (currentConversationWindow != null)
        {
            Destroy(currentConversationWindow);
            currentConversationWindow = null;
        }

        // Open the new conversation window
        Card cardComponent = card.GetComponent<Card>();
        currentConversationWindow = cardComponent.ShowConversation();

        // Make sure the clicked card remains clickable
        cardComponent.SetClickable(true);

        // Enable the next card if there is one
        if (currentIndex < cards.Length - 1)
        {
            currentIndex++;
            SetCardClickable(currentIndex);
        }
    }

    void SetCardClickable(int index)
    {
        cards[index].GetComponent<Card>().SetClickable(true);
    }
}