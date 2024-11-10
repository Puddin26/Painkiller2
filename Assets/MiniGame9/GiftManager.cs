using UnityEngine;
using System.Collections.Generic;

public class GiftManager : SnapManager
{
    public List<SimplifiedObjectMoveAndSnap> wrapPapers;
    public List<SimplifiedObjectMoveAndSnap> ribbons;
    public SimplifiedObjectMoveAndSnap birthdayCard;

    public GameObject noteObject;  // The note object to change sprite at the end
    public Sprite completedNoteSprite;  // Sprite for completed note

    public GameObject boxObject;  // The box object to change sprite when wrap is snapped
    public List<Sprite> wrapSprites;  // Sprites corresponding to each wrap in inspector

    public Vector2 ribbonPosition;  // Single position for placing the ribbon sprite
    public List<Sprite> ribbonSprites;  // List of sprites for each ribbon

    private int currentStage = 0; // 0 = Wrap Papers, 1 = Ribbons, 2 = Card
    
    public int CurrentStage => currentStage; // Exposes currentStage as a read-only property

    public Follower follower;

    protected override void HandleObjectSnapped(SimplifiedObjectMoveAndSnap snappedObject)
    {
        if (currentStage == 0 && wrapPapers.Contains(snappedObject))
        {
            base.HandleObjectSnapped(snappedObject);
            ChangeBoxSprite(snappedObject);  // Change box sprite based on wrap
            Destroy(snappedObject.gameObject);  // Remove snapped wrap paper
            
            currentStage = 1; // Move to the next stage
        }
        else if (currentStage == 1 && ribbons.Contains(snappedObject))
        {
            base.HandleObjectSnapped(snappedObject);
            PlaceRibbonSprite(snappedObject);  // Place the specific ribbon sprite at the defined position
            Destroy(snappedObject.gameObject);  // Remove snapped ribbon
            
            currentStage = 2; // Move to the final stage
        }
        else if (currentStage == 2 && snappedObject == birthdayCard)
        {
            base.HandleObjectSnapped(snappedObject);
            ChangeNoteSprite();  // Change note sprite when all stages are done
        }
        else
        {
            Debug.Log("Cannot snap this object yet.");
        }
    }

    private void ChangeBoxSprite(SimplifiedObjectMoveAndSnap snappedObject)
    {
        int wrapIndex = wrapPapers.IndexOf(snappedObject);
        if (wrapIndex >= 0 && wrapIndex < wrapSprites.Count)
        {
            boxObject.GetComponent<SpriteRenderer>().sprite = wrapSprites[wrapIndex];
        }
    }

    private void PlaceRibbonSprite(SimplifiedObjectMoveAndSnap snappedObject)
    {
        int ribbonIndex = ribbons.IndexOf(snappedObject);
        if (ribbonIndex >= 0 && ribbonIndex < ribbonSprites.Count)
        {
            GameObject ribbonSpriteObject = new GameObject("RibbonSprite");
            ribbonSpriteObject.transform.position = ribbonPosition;

            // Ensure the ribbon sprite appears on top of the box
            SpriteRenderer spriteRenderer = ribbonSpriteObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = ribbonSprites[ribbonIndex];
            spriteRenderer.sortingOrder = 1; // Set sorting order to ensure it renders above the box
        }
    }

    private void ChangeNoteSprite()
    {
        if (noteObject != null && completedNoteSprite != null)
        {
            noteObject.GetComponent<SpriteRenderer>().sprite = completedNoteSprite;
            follower.nextPage = true;
        }
    }

    private bool AllObjectsSnappedInList(List<SimplifiedObjectMoveAndSnap> objects)
    {
        foreach (var obj in objects)
        {
            if (!snappedObjects.Contains(obj))
                return false;
        }
        return true;
    }
}
