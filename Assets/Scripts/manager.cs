using UnityEngine;
using System.Collections.Generic;

public class SnapManager : MonoBehaviour
{
    public int totalObjects = 4;
    protected HashSet<SimplifiedObjectMoveAndSnap> snappedObjects = new HashSet<SimplifiedObjectMoveAndSnap>();


    void OnEnable()
    {
        SimplifiedObjectMoveAndSnap.OnObjectSnapped += HandleObjectSnapped;
    }

    void OnDisable()
    {
        SimplifiedObjectMoveAndSnap.OnObjectSnapped -= HandleObjectSnapped;
    }

    protected virtual void HandleObjectSnapped(SimplifiedObjectMoveAndSnap snappedObject)
    {
        if (!snappedObjects.Contains(snappedObject))
        {
            snappedObjects.Add(snappedObject);
            //Things after each time new object snapped in place
        }

        if (snappedObjects.Count == totalObjects)
        {
            //Things after all objects snapped in place
        }
    }
}