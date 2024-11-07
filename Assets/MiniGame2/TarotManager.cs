using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TarotManager : SnapManager
{
    public TextMeshProUGUI arcanaText;
    public int snappedCount = 18;
    
    //References
    public GameObject note;
    public Sprite finishedNote;
    
    protected override void HandleObjectSnapped(SimplifiedObjectMoveAndSnap snappedObject)
    {
        if (!snappedObjects.Contains(snappedObject))
        {
            snappedObjects.Add(snappedObject);
            snappedCount++;
            arcanaText.text = $"Major Arcana ({snappedCount}/22)";
        }

        if (snappedObjects.Count == totalObjects)
        {
            // Change the note sprite once all cards are in place
            if (note != null && finishedNote != null)
            {
                note.GetComponent<SpriteRenderer>().sprite = finishedNote;
            }
        }
    }
}
