using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG4Gretchen : MonoBehaviour
{
    public GameObject MG4_Camera, MG4_Mathew, MG4_text;
    public bool MG4_canInteract, MG4_isLocked;
    public float MG4_distance, MG4_Appear, MG4_walkSpeed;

    public Sprite turnaround;

    private SpriteRenderer MG4_mathewRenderer;

    // Start is called before the first frame update
    void Start()
    {
        MG4_mathewRenderer = MG4_Mathew.GetComponent<SpriteRenderer>();
        Invoke("DeleteText", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(!MG4_isLocked)
        {
            if (Input.GetKey(KeyCode.W)) { MG4_Camera.transform.Translate(Vector3.forward * MG4_walkSpeed * Time.deltaTime); }
            if (Input.GetKey(KeyCode.S)) { MG4_Camera.transform.Translate(Vector3.back * MG4_walkSpeed * Time.deltaTime); }

            if (Vector3.Distance(gameObject.transform.position, MG4_Camera.transform.position) < MG4_distance) { gameObject.GetComponent<SpriteRenderer>().color = Color.red; MG4_canInteract = true; }
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
            print("Look Back");
        }
    }

    private void DeleteText()
    {
        Destroy(MG4_text);
    }
}
