using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG4Gretchen : MonoBehaviour
{
    public GameObject MG4_Camera, MG4_Mathew, NPCs, walker;
    public bool MG4_canInteract, MG4_isLocked;
    public float MG4_distance, MG4_Appear, MG4_walkSpeed;
    public Follower followr;

    public Sprite turnaround;

    private SpriteRenderer MG4_mathewRenderer;

    // Start is called before the first frame update
    void Start()
    {
        MG4_mathewRenderer = MG4_Mathew.GetComponent<SpriteRenderer>();
        MG4_mathewRenderer.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!MG4_isLocked)
        {
            if (Vector3.Distance(gameObject.transform.position, MG4_Camera.transform.position) < MG4_distance) { gameObject.GetComponent<SpriteRenderer>().color = Color.red; MG4_canInteract = true;  }
            else { MG4_canInteract = false; }
        }
        else
        {
           if(MG4_mathewRenderer.color.a <= 1)
           {
                MG4_mathewRenderer.color += new Color(0, 0, 0, MG4_Appear * Time.deltaTime);
           }
        }
    }

    private void OnMouseDown()
    {
        if(MG4_canInteract)
        {
            MG4_isLocked = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = turnaround;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            followr.nextPage = true;
            Destroy(walker);
            print("Look Back");
        }
    }

    public void pressWalk()
    {
        Camera.main.orthographic = false;
        if (!MG4_isLocked)
        {
            NPCs.transform.Translate(Vector3.back * MG4_walkSpeed * Time.deltaTime);
        }
    }
}
