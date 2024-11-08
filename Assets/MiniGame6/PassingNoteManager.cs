using UnityEngine;

public class PassingNoteManager : MonoBehaviour
{
    public enum GameStage { Stage1, Stage2, Stage3 /* Add more stages as needed */ }
    public GameStage currentStage = GameStage.Stage1;

    public SequencesManager sequencesManager; // Reference to SequencesManager for Stage 1

    private void Start()
    {
        SetStage(currentStage);
    }

    public void SetStage(GameStage stage)
    {
        currentStage = stage;
        switch (currentStage)
        {
            case GameStage.Stage1:
                if (sequencesManager != null)
                {
                    sequencesManager.enabled = true;
                }
                break;
            // Add additional cases for other stages as needed
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