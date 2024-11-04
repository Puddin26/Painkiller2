using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineDrawer : MonoBehaviour
{
    //Line
    public LineRenderer line;
    public GameObject Reciever;
    public bool canDrawAnother;
    public bool isConnected;
    private Vector3 previousPosition;
    [SerializeField] float minDistance = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        previousPosition = transform.position;
    }

    public void StartLine(Vector2 position)
    {
        if(!isConnected)
        {
            line.positionCount = 1;
            line.SetPosition(0, position);
            MG3Manager.MG3CurrentGameobject = this.gameObject;
        }
    }

    // Update is called once per frame
    public void UpdateLine()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = -3.28f;

            if(Vector3.Distance(currentPosition, previousPosition) > minDistance && !isConnected && MG3Manager.MG3CanDraw)
            {
                if(previousPosition == transform.position)
                {
                    line.SetPosition(0, currentPosition);
                }
                else
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount-1, currentPosition);
                }

                line.positionCount++;
                line.SetPosition(line.positionCount-1, currentPosition);
                previousPosition = currentPosition;
            }
        }
    }

    public void CheckLine()
    {
        if(Reciever == MG3Manager.MG3CurrentGameobject && MG3Manager.MG3CanDraw)
        {
            if (!canDrawAnother)
            {
                isConnected = true;
            }
            Reciever.GetComponent<LineDrawer>().isConnected = true;
            print(MG3Manager.MG3CurrentGameobject.name);
            print("Connected");
        }
        else if(!Reciever.GetComponent<LineDrawer>().isConnected && MG3Manager.MG3CurrentGameobject != gameObject && MG3Manager.MG3CurrentGameobject != null)
        {
            LineDrawer CurrentLine = MG3Manager.MG3CurrentGameobject.GetComponent<LineDrawer>();
            CurrentLine.line.positionCount = 1;
            CurrentLine.line.SetPosition(0, MG3Manager.MG3CurrentGameobject.transform.position);
            MG3Manager.MG3CanDraw = false;
            print("WrongDot");
        }
        else if(MG3Manager.MG3CurrentGameobject != gameObject && MG3Manager.MG3CurrentGameobject != null)
        {
            LineDrawer CurrentLine = MG3Manager.MG3CurrentGameobject.GetComponent<LineDrawer>();
            CurrentLine.line.positionCount = 1;
            CurrentLine.line.SetPosition(0, MG3Manager.MG3CurrentGameobject.transform.position);
            MG3Manager.MG3CanDraw = false;
            print("WrongDot");
        }
    }
}
