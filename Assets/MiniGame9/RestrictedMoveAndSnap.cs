using UnityEngine;

public class RestrictedMoveAndSnap : SimplifiedObjectMoveAndSnap
{
    public GiftManager giftManager;

    protected override void Update()
    {
        if (IsMovementAllowed())
        {
            base.Update();
        }
        else
        {
            Debug.Log("Movement restricted until the correct stage.");
        }
    }

    private bool IsMovementAllowed()
    {
        if (giftManager == null)
        {
            Debug.LogWarning("GiftManager not assigned to RestrictedMoveAndSnap.");
            return false;
        }

        // Check the stage and allow movement only for the relevant objects
        if (giftManager.CurrentStage == 0 && giftManager.wrapPapers.Contains(this)) return true;
        if (giftManager.CurrentStage == 1 && giftManager.ribbons.Contains(this)) return true;
        if (giftManager.CurrentStage == 2 && this == giftManager.birthdayCard) return true;

        return false;
    }
}