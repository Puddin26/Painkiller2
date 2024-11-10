using UnityEngine;

public class PassingNoteManager : MonoBehaviour
{
    public enum GameStage { Stage1, Stage2, Stage3 /* Add more stages as needed */ }
    public GameStage currentStage = GameStage.Stage1;

    public GameObject stage1Object; // Object for Stage 1
    public GameObject stage2Object; // Object for Stage 2
    public GameObject stage3Object; // Object for Stage 3

    public Follower follower;

    public bool startStage;

    private void Update()
    {
        if (Camera.main.transform.position.y < -29 && !startStage)
        {
            SetStage(currentStage);
            startStage = true;
        }
    }

    public void SetStage(GameStage stage)
    {
        currentStage = stage;

        // Deactivate all stage objects initially
        if (stage1Object != null) stage1Object.SetActive(false);
        if (stage2Object != null) stage2Object.SetActive(false);
        if (stage3Object != null) stage3Object.SetActive(false);

        // Activate only the object for the current stage
        switch (currentStage)
        {
            case GameStage.Stage1:
                if (stage1Object != null)
                {
                    stage1Object.SetActive(true);
                }
                break;

            case GameStage.Stage2:
                if (stage2Object != null)
                {
                    stage2Object.SetActive(true);
                }
                break;

            case GameStage.Stage3:
                if (stage3Object != null)
                {
                    stage3Object.SetActive(true);
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
            follower.nextPage = true;
        }
    }
}