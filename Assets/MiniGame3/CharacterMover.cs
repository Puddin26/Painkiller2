using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public LineDrawer lineDrawer;

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
        MG3Manager.MG3CurrentGameobject = null; 
        MG3Manager.MG3CanDraw = true;
    }

}
