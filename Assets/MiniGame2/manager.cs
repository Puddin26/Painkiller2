using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int totalObjects = 4;
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
        snappedObjects.Add(snappedObject);
        
        if (snappedObjects.Count == totalObjects)
        {
            // Actions to be triggered when all objects are snapped
        }
    }
}