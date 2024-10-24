using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG5CharacterMover : MonoBehaviour
{
    public MG5LineDrawer lineDrawer;

    private void OnMouseDown()
    {
        lineDrawer.StartLine(transform.position);
    }

    private void OnMouseDrag()
    {
        lineDrawer.UpdateLine();
    }

    private void OnMouseOver()
    {
        lineDrawer.CheckLine();
    }

    private void OnMouseUp()
    {
        lineDrawer.StartLine(transform.position);
        MG5Manager.MG5CurrentGameobject = null; 
        MG5Manager.MG5CanDraw = true;
    }

}
