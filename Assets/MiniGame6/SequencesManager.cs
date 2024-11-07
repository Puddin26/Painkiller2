using System.Collections;
using UnityEngine;

public class SequencesManager : MonoBehaviour
{
    public enum GameStage { Stage1, Stage2, Stage3 /* Add more as needed */ }
    
    public GameStage currentStage = GameStage.Stage1; // Start at the first stage
    public SpriteRenderer targetSpriteRenderer; // Assign in Inspector
    public Sprite[] sprites; // Assign in Inspector, with the paper as the last sprite
    public float interval = 2f; // Time interval between sprite changes
    public float fadeDuration = 0.5f; // Duration of fade-in and fade-out
    public DrawLine drawLineScript; // Reference to the DrawLine script
    public Vector2 clickPosition; // Position to check for advancing stages
    public float clickRange = 1f; // Range within which the click is valid for advancing
    
    public GameObject nextButton;

    private int currentSpriteIndex = 0;
    private bool canAdvance = false;

    private void Start()
    {
        if (targetSpriteRenderer == null || sprites.Length == 0 || drawLineScript == null)
        {
            Debug.LogError("Please assign the SpriteRenderer, Sprites, and DrawLine script in the Inspector.");
            return;
        }
        drawLineScript.enabled = false; // Start with drawing disabled
        StartCoroutine(SpriteChangeRoutine());
    }

    private void Update()
    {
        if (canAdvance && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mousePosition, clickPosition) <= clickRange)
            {
                AdvanceToNextStage();
            }
        }
    }

    private IEnumerator SpriteChangeRoutine()
    {
        while (currentSpriteIndex < sprites.Length)
        {
            yield return StartCoroutine(FadeOut());
            ChangeSprite();
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(interval);
        }
    }

    private void ChangeSprite()
    {
        targetSpriteRenderer.sprite = sprites[currentSpriteIndex];
        
        // Check if this is the last sprite (the paper)
        if (currentSpriteIndex == sprites.Length - 1)
        {
            drawLineScript.enabled = true; // Enable drawing on the last sprite
            nextButton.SetActive(true);
            canAdvance = true; // Allow advancing once drawing is done
        }
        
        currentSpriteIndex++;
    }

    private IEnumerator FadeOut()
    {
        Color color = targetSpriteRenderer.color;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            targetSpriteRenderer.color = color;
            yield return null;
        }
        color.a = 0f;
        targetSpriteRenderer.color = color;
    }

    private IEnumerator FadeIn()
    {
        Color color = targetSpriteRenderer.color;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            targetSpriteRenderer.color = color;
            yield return null;
        }
        color.a = 1f;
        targetSpriteRenderer.color = color;
    }

    private void AdvanceToNextStage()
    {
        switch (currentStage)
        {
            case GameStage.Stage1:
                currentStage = GameStage.Stage2;
                SetupStage2();
                break;
            case GameStage.Stage2:
                currentStage = GameStage.Stage3;
                SetupStage3();
                break;
            // Add cases for additional stages here
            default:
                Debug.Log("No further stages available.");
                break;
        }
    }

    private void SetupStage2()
    {
        Debug.Log("Setting up Stage 2");
        // Configure elements for Stage 2 (e.g., new objects, disable DrawLine, etc.)
        drawLineScript.enabled = false;
        canAdvance = false; // Reset advance condition for the next stage
        // Additional Stage 2 setup logic here
    }

    private void SetupStage3()
    {
        Debug.Log("Setting up Stage 3");
        // Configure elements for Stage 3
        // Example: activate new game objects, change scene elements, etc.
    }
}
