using UnityEngine;

public class CalendarManager : SnapManager
{
    protected override void HandleObjectSnapped(SimplifiedObjectMoveAndSnap snappedObject)
    {
        base.HandleObjectSnapped(snappedObject);

        // Check if all objects are snapped in place
        if (snappedObjects.Count == totalObjects)
        {
            // Things after all objects snapped in place
            Debug.Log("Finished");
        }
    }
}