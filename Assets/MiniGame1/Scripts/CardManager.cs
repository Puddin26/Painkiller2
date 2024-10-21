using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards;    // Array to hold card objects
    public float spacing = 2.0f;  // Horizontal spacing between cards
    public float cardYPosition = 0.0f;  // Adjustable Y position for cards

    private int currentIndex = 0;

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
        // Check if the card clicked is the one that should be clicked
        if (cards[currentIndex] == card)
        {
            currentIndex++;

            // If there are more cards, enable the next one
            if (currentIndex < cards.Length)
            {
                Debug.Log(cards[currentIndex]);
                SetCardClickable(currentIndex);
            }
        }
    }

    void SetCardClickable(int index)
    {
        cards[index].GetComponent<Card>().SetClickable(true);
    } 
    
    
}