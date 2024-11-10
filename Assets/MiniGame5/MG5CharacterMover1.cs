using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG5CharacterMover1 : MonoBehaviour
{
    public MG5LineDrawer2 lineDrawer;

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
