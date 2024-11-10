using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public LineDrawer lineDrawer;
    public MG33Manager manager;
    private LineDrawer drawer;
    public Follower follower;
    private bool allDone;
    private bool gameDone;

    private void OnMouseDown()
    {
        lineDrawer.StartLine(transform.position);
    }

    private void OnMouseDrag()
    {
        lineDrawer.UpdateLine();
        AudioManager.instance.ScribbleNotes();
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

    private void Update()
    {
        if(!gameDone)
        {
            allDone = true;
        }

        foreach (var item in manager.dots)
        {
            drawer = item.GetComponent<LineDrawer>();
            if(drawer.isConnected != true)
            {
                allDone = false;
            }
        }

        if(allDone)
        {
            follower.nextPage = true;
            allDone = false;
            gameDone = true;
        }
    }

}
