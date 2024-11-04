using UnityEngine;
using TMPro; // Use this for TextMeshPro
using System.Collections.Generic;
using Room1;

public class GameManager : MonoBehaviour
{
    public int totalObjects = 4;
    public int snappedCount;
    public TextMeshProUGUI arcanaText;
    private HashSet<SimplifiedObjectMoveAndSnap> snappedObjects = new HashSet<SimplifiedObjectMoveAndSnap>();
    public Room1.CharacterMover characterMover;

    // Add references for the object and the new sprite
    public GameObject targetObject;
    public Sprite newSprite;

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
            if(targetObject != null && newSprite != null)
            {
                targetObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            }

            characterMover.tableDone = 2;
        }
    }
}