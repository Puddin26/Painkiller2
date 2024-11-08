using UnityEngine;

public class PassingNoteManager : MonoBehaviour
{
    public enum GameStage { Stage1, Stage2, Stage3 /* Add more stages as needed */ }
    public GameStage currentStage = GameStage.Stage1;

    public SequencesManager sequencesManager; // Reference to SequencesManager for Stage 1
    public Drawings drawingsScript; // Reference to Drawings script for Stage 2

    private void Start()
    {
        SetStage(currentStage);
    }

    public void SetStage(GameStage stage)
    {
        currentStage = stage;

        // Disable all stage scripts initially
        if (sequencesManager != null) sequencesManager.enabled = false;
        if (drawingsScript != null) drawingsScript.enabled = false;

        // Activate only the script for the current stage
        switch (currentStage)
        {
            case GameStage.Stage1:
                if (sequencesManager != null)
                {
                    sequencesManager.enabled = true;
                }
                break;

            case GameStage.Stage2:
                if (drawingsScript != null)
                {
                    drawingsScript.enabled = true;
                }
                break;

            // Additional cases can be added for other stages as needed
            default:
                Debug.LogWarning("Stage not defined for this game stage");
                break;
        }
    }

    public void AdvanceToNextStage()
    {
        if (currentStage < GameStage.Stage3)
        {
            currentStage++;
            SetStage(currentStage);
        }
        else
        {
            Debug.Log("Final stage reached");
        }
    }
}