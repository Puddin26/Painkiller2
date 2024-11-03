using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGame13;

namespace MiniGame13
{
    public class MouseOverScript : MonoBehaviour
    {

        private void OnMouseDrag()
        {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 0;
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 11.5f));

        }

    }
}


