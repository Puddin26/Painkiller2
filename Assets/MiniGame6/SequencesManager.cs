using System.Collections;
using UnityEngine;

public class SequencesManager : MonoBehaviour
{
    public SpriteRenderer targetSpriteRenderer; // Assign in Inspector
    public Sprite[] sprites; // Assign in Inspector, with the paper as the last sprite
    public float interval = 2f; // Time interval between sprite changes
    public float fadeDuration = 0.5f; // Duration of fade-in and fade-out
    public PassingNoteManager passingNoteManager; // Reference to PassingNoteManager to manage stage progression
    
    private int currentSpriteIndex = 0;

    private void Start()
    {
        if (targetSpriteRenderer == null || sprites.Length == 0 || passingNoteManager == null)
        {
            Debug.LogError("Please assign the SpriteRenderer, Sprites, DrawLine script, PassingNoteManager, and finalObject in the Inspector.");
            return;
        }

        // Ensure the targetSpriteRenderer is active
        if (!targetSpriteRenderer.gameObject.activeInHierarchy)
        {
            targetSpriteRenderer.gameObject.SetActive(true);
        }
        

        StartCoroutine(SpriteChangeRoutine());
    }

    private IEnumerator SpriteChangeRoutine()
    {
        while (currentSpriteIndex < sprites.Length)
        {
            // Fade-out
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
                SetSpriteAlpha(alpha);
                yield return null;
            }

            // Change sprite
            targetSpriteRenderer.sprite = sprites[currentSpriteIndex];

            currentSpriteIndex++;

            // Fade-in
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
                SetSpriteAlpha(alpha);
                yield return null;
            }

            yield return new WaitForSeconds(interval);
        }

        // Sequence finished, advance to the next stage
        passingNoteManager.AdvanceToNextStage();
    }

    private void SetSpriteAlpha(float alpha)
    {
        if (targetSpriteRenderer != null)
        {
            Color color = targetSpriteRenderer.color;
            color.a = alpha;
            targetSpriteRenderer.color = color;
        }
    }
}
