using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{

    private float cameraShift = 0;
    private bool canMove;
    public bool nextPage;
    public GameObject arrow;
    public bool letsmove;


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

        if(nextPage)
        {
            arrow.SetActive(true);
        }
    }


    public void MoveDown()
    {
        canMove = true;
        cameraShift -= 10;
        Camera.main.orthographic = true;
        arrow.SetActive(false);
        nextPage = false;
    }
}
