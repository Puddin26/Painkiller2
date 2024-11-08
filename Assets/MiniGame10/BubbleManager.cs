using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubblePrefab;
    public List<Vector2> spawnPositions;
    public float spawnInterval = 2f;
    public TMP_Text countdownText;
    public TMP_Text resultText;
    
    public Sprite phoneCallSprite; // Sprite when receiving a phone call
    public Sprite phoneOffSprite; // Sprite when the phone is off
    public GameObject phoneObject; // GameObject representing the phone
    public Vector2 declineButtonPosition; // Position of the decline button
    public float declineButtonRadius = 0.5f;

    private List<GameObject> activeBubbles = new List<GameObject>();
    private float timer = 30f;
    private float spawnTimer;
    private bool gameEnded = false;
    private bool phoneCallActive = false;
    private float nextPhoneCallTime;

    void Start()
    {
        spawnTimer = spawnInterval;
        resultText.text = "";
        ScheduleNextPhoneCall();
        SetPhoneState(false); // Start with phone off
    }

    void Update()
    {
        if (gameEnded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        // Handle phone call detection if active
        if (phoneCallActive)
        {
            CheckForDeclineButtonClick();
        }

        // Check for incoming phone calls
        if (Time.time >= nextPhoneCallTime && !phoneCallActive)
        {
            ReceivePhoneCall();
        }

        // Countdown timer
        timer -= Time.deltaTime;
        countdownText.text = Mathf.Ceil(timer).ToString();

        if (timer <= 0)
        {
            EndGame("You Win!");
        }

        // Bubble spawn logic
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnBubble();
            spawnTimer = spawnInterval;
        }

        // Check for game over condition (list is full)
        if (activeBubbles.Count >= spawnPositions.Count)
        {
            EndGame("Try Again");
        }
    }


    void CheckForDeclineButtonClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if click is within decline button radius
            if (Vector2.Distance(mousePosition, declineButtonPosition) <= declineButtonRadius)
            {
                DeclinePhoneCall();
            }
        }
    }

    void ReceivePhoneCall()
    {
        phoneCallActive = true;
        SetPhoneState(true); // Change to phone call state
    }

    void DeclinePhoneCall()
    {
        phoneCallActive = false;
        SetPhoneState(false); // Change back to phone off state
        ScheduleNextPhoneCall(); // Schedule the next phone call
    }

    void SetPhoneState(bool isActive)
    {
        // Change the phone's appearance and activation state based on whether a call is active
        SpriteRenderer phoneSpriteRenderer = phoneObject.GetComponent<SpriteRenderer>();
        phoneSpriteRenderer.sprite = isActive ? phoneCallSprite : phoneOffSprite;
    }

    void ScheduleNextPhoneCall()
    {
        // Schedule the next phone call to happen in 2 to 6 seconds
        nextPhoneCallTime = Time.time + Random.Range(2f, 6f);
    }
    
    public bool IsPhoneCallActive()
    {
        return phoneCallActive;
    }

    void SpawnBubble()
    {
        if (spawnPositions.Count == 0) return;

        List<Vector2> availablePositions = new List<Vector2>();

        foreach (Vector2 position in spawnPositions)
        {
            bool isOccupied = false;

            foreach (GameObject Bubble in activeBubbles)
            {
                if (Vector2.Distance(Bubble.transform.position, position) < 0.1f)
                {
                    isOccupied = true;
                    break;
                }
            }

            if (!isOccupied)
            {
                availablePositions.Add(position);
            }
        }

        if (availablePositions.Count == 0) return;

        Vector2 randomPosition = availablePositions[Random.Range(0, availablePositions.Count)];
        GameObject bubble = Instantiate(bubblePrefab, randomPosition, Quaternion.identity);
        activeBubbles.Add(bubble);

        bubble.GetComponent<Bubble>().SetManager(this);
    }

    public void RemoveBubble(GameObject bubble)
    {
        if (activeBubbles.Contains(bubble))
        {
            activeBubbles.Remove(bubble);
            Destroy(bubble);
        }
    }

    void EndGame(string message)
    {
        gameEnded = true;
        resultText.text = message;
    }
}