using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackUp : MonoBehaviour
{
    public int order;
    public bool locked;
    public Vector2 og_position;
    public Vector2 snap_position;
    public static int current_order = 0;

    public Follower follower;


    private void OnMouseDrag()
    {
        if (!locked)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 5;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z));
        }
    }

    private void OnMouseUp()
    {

        if(Vector2.Distance(snap_position, transform.position) < 1f)
        {
            if (order > current_order)
            {
                transform.position = og_position;
            }
            else
            {
                transform.position = new Vector3(snap_position.x, snap_position.y, transform.position.z - order);
                AudioManager.instance.Pack();
                current_order++;
                locked = true;
            }

            if(current_order == 5)
            {
                follower.nextPage = true;
            }
        }
    }
}
