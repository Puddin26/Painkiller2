using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class OrderedSnappingManager : SnapManager
{
    // Enum to track the order of snapping
    private enum SnappingStep { WrapPaper, Ribbon, BirthdayCard }
    private SnappingStep currentStep = SnappingStep.WrapPaper;

    // Lists to hold the different types of items
    public List<SimplifiedObjectMoveAndSnap> wrapPapers;
    public List<SimplifiedObjectMoveAndSnap> ribbons;
    public SimplifiedObjectMoveAndSnap birthdayCard;

    // Override the method to handle object snapping in order
    protected override void HandleObjectSnapped(SimplifiedObjectMoveAndSnap snappedObject)
    {
        // Check if the object is allowed to snap in the current step
        switch (currentStep)
        {
            case SnappingStep.WrapPaper:
                if (wrapPapers.Contains(snappedObject))
                {
                    ProceedToNextStep();
                }
                else
                {
                    return; // Early return if an incorrect object is snapped
                }
                break;

            case SnappingStep.Ribbon:
                if (ribbons.Contains(snappedObject))
                {
                    ProceedToNextStep();
                }
                else
                {
                    return; // Early return if an incorrect object is snapped
                }
                break;

            case SnappingStep.BirthdayCard:
                if (snappedObject == birthdayCard)
                {
                    ProceedToNextStep();
                }
                else
                {
                    return; // Early return if an incorrect object is snapped
                }
                break;
        }

        // If the snapped object is correct, continue with the base functionality
        base.HandleObjectSnapped(snappedObject);
    }

    // Method to proceed to the next step
    private void ProceedToNextStep()
    {
        switch (currentStep)
        {
            case SnappingStep.WrapPaper:
                currentStep = SnappingStep.Ribbon;
                break;
            case SnappingStep.Ribbon:
                currentStep = SnappingStep.BirthdayCard;
                break;
            case SnappingStep.BirthdayCard:
                Debug.Log("All items have been snapped in order!");
                break;
        }
    }
}
