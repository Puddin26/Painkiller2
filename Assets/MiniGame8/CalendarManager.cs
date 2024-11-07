using UnityEngine;
using TMPro; 
using System.Collections.Generic;

public class CalendarManager : MonoBehaviour
{
    public int totalObjects = 4;
    public int snappedCount;
    public TextMeshProUGUI arcanaText;
    private HashSet<SimplifiedObjectMoveAndSnap> snappedObjects = new HashSet<SimplifiedObjectMoveAndSnap>();

    void OnEnable()
    {
        SimplifiedObjectMoveAndSnap.OnObjectSnapped += HandleObjectSnapped;
    }

    void OnDisable()
    {
        SimplifiedObjectMoveAndSnap.OnObjectSnapped -= HandleObjectSnapped;
    }

    void HandleObjectSnapped(SimplifiedObjectMoveAndSnap snappedObject)
    {
        if (!snappedObjects.Contains(snappedObject))
        {
            snappedObjects.Add(snappedObject);
            snappedCount++;
            arcanaText.text = $"Major Arcana ({snappedCount}/22)";
        }

        if (snappedObjects.Count == totalObjects)
        {
            // Actions when all objects are snapped, if needed
        }
    }
}