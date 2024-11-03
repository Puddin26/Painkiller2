using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{

    private float cameraShift = 0;
    private bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if(transform.position.y != cameraShift)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, cameraShift, transform.position.z), 1f * Time.deltaTime * 1.5f);
            }
            else
            {
                canMove = false;
            }
        }
    }


    public void MoveDown()
    {
        canMove = true;
        cameraShift -= 10; 
    }
}
