using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MG5LineDrawer : MonoBehaviour
{
    //Line
    public LineRenderer MG5_line;
    public GameObject MG5_Reciever;
    public bool MG5_canDrawAnother;
    private bool MG5_isConnected;
    private Vector3 MG5_previousPosition;
    [SerializeField] float MG5_minDistance = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        MG5_line = GetComponent<LineRenderer>();
        MG5_line.positionCount = 1;
        MG5_previousPosition = transform.position;
    }

    public void StartLine(Vector2 position)
    {
        if(!MG5_isConnected)
        {
            MG5_line.positionCount = 1;
            MG5_line.SetPosition(0, position);
            MG5Manager.MG5CurrentGameobject = this.gameObject;
        }
    }

    // Update is called once per frame
    public void UpdateLine()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0;

            if(Vector3.Distance(currentPosition, MG5_previousPosition) > MG5_minDistance && !MG5_isConnected && MG5Manager.MG5CanDraw)
            {
                if(MG5_previousPosition == transform.position)
                {
                    MG5_line.SetPosition(0, currentPosition);
                }
                else
                {
                    MG5_line.positionCount++;
                    MG5_line.SetPosition(MG5_line.positionCount-1, currentPosition);
                }

                MG5_line.positionCount++;
                MG5_line.SetPosition(MG5_line.positionCount-1, currentPosition);
                MG5_previousPosition = currentPosition;
            }
        }
    }

    public void CheckLine()
    {
        if(MG5_Reciever == MG5Manager.MG5CurrentGameobject && MG5Manager.MG5CanDraw)
        {
            if (!MG5_canDrawAnother)
            {
                MG5_isConnected = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
            MG5_Reciever.GetComponent<MG5LineDrawer>().MG5_isConnected = true;
            MG5_Reciever.GetComponent<SpriteRenderer>().color = Color.black;
            print(MG5Manager.MG5CurrentGameobject.name);
            print("Connected");
        }
        else if(!MG5_Reciever.GetComponent<MG5LineDrawer>().MG5_isConnected && MG5Manager.MG5CurrentGameobject != gameObject && MG5Manager.MG5CurrentGameobject != null)
        {
            MG5LineDrawer CurrentLine = MG5Manager.MG5CurrentGameobject.GetComponent<MG5LineDrawer>();
            CurrentLine.MG5_line.positionCount = 1;
            CurrentLine.MG5_line.SetPosition(0, MG5Manager.MG5CurrentGameobject.transform.position);
            MG5Manager.MG5CanDraw = false;
            print("WrongDot");
        }
        else if(MG5Manager.MG5CurrentGameobject != gameObject && MG5Manager.MG5CurrentGameobject != null)
        {
            MG5LineDrawer CurrentLine = MG5Manager.MG5CurrentGameobject.GetComponent<MG5LineDrawer>();
            CurrentLine.MG5_line.positionCount = 1;
            CurrentLine.MG5_line.SetPosition(0, MG5Manager.MG5CurrentGameobject.transform.position);
            MG5Manager.MG5CanDraw = false;
            print("WrongDot");
        }
    }
}
