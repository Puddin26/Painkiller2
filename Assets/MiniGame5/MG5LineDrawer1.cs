using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MG5LineDrawer1 : MonoBehaviour
{

    //Line
    public LineRenderer MG5_line;
    public static bool EndisDrawing;
    public static GameObject EndObject;
    private Vector3 MG5_previousPosition;
    private int RanNum;
    [SerializeField] float MG5_minDistance = 0.01f;
    [SerializeField] GameObject Scribble, Heart;

    // Start is called before the first frame update
    void Start()
    {
        MG5_line = GetComponent<LineRenderer>();
        MG5_line.positionCount = 1;
        MG5_previousPosition = transform.position;
    }

    public void StartLine(Vector2 position)
    {
            MG5_line.positionCount = 1;
            MG5_line.SetPosition(0, position);
    }

    private void OnMouseDown()
    {
        StartLine(transform.position);
        EndObject = gameObject;
        EndisDrawing = true;
    }

    // Update is called once per frame
    private void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            AudioManager.instance.ScribbleNotes();
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = -5;

            if (Vector3.Distance(currentPosition, MG5_previousPosition) > MG5_minDistance)
            {
                if (MG5_previousPosition == transform.position)
                {
                    MG5_line.SetPosition(0, currentPosition);
                }
                else
                {
                    MG5_line.positionCount++;
                    MG5_line.SetPosition(MG5_line.positionCount - 1, currentPosition);
                }

                MG5_line.positionCount++;
                MG5_line.SetPosition(MG5_line.positionCount - 1, currentPosition);
                MG5_previousPosition = currentPosition;
            }
        }
    }

    private void OnMouseUp()
    {
        StartLine(transform.position);
        EndisDrawing = false;
        EndObject = null;
    }

    private void OnMouseEnter()
    {
        if(EndisDrawing)
        {
            RanNum = Random.Range(0, 2);
            AudioManager.instance.LinkCon();
            if(RanNum == 0) { Heart.SetActive(true); }
            else { Scribble.SetActive(true); }
            if(!IsInvoking("Dissapear")) { Invoke("Dissapear", 1); }

            EndObject.GetComponent<MG5LineDrawer1>().StartLine(transform.position);
            EndObject.SetActive(false);
            gameObject.SetActive(false);
            EndObject = null;
            EndisDrawing = false;
        }
    }

    private void Dissapear()
    {
        Heart.SetActive(false);
        Scribble.SetActive(false);
    }
}
