using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MG5LineDrawer2 : MonoBehaviour
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
            MG5Manager1.MG5CurrentGameobject1 = this.gameObject;
        }
    }

    // Update is called once per frame
    public void UpdateLine()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0;

            if(Vector3.Distance(currentPosition, MG5_previousPosition) > MG5_minDistance && !MG5_isConnected && MG5Manager1.MG5CanDraw1)
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
        if(MG5_Reciever == MG5Manager1.MG5CurrentGameobject1 && MG5Manager1.MG5CanDraw1)
        {
            if (!MG5_canDrawAnother)
            {
                MG5_isConnected = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
            MG5_Reciever.GetComponent<MG5LineDrawer2>().MG5_isConnected = true;
            MG5_Reciever.GetComponent<SpriteRenderer>().color = Color.black;
            print(MG5Manager1.MG5CurrentGameobject1.name);
            AudioManager.instance.LinkCon();
            print("Connected");
        }
        else if(!MG5_Reciever.GetComponent<MG5LineDrawer2>().MG5_isConnected && MG5Manager1.MG5CurrentGameobject1 != gameObject && MG5Manager1.MG5CurrentGameobject1 != null)
        {
            MG5LineDrawer2 CurrentLine = MG5Manager1.MG5CurrentGameobject1.GetComponent<MG5LineDrawer2>();
            CurrentLine.MG5_line.positionCount = 1;
            CurrentLine.MG5_line.SetPosition(0, MG5Manager1.MG5CurrentGameobject1.transform.position);
            MG5Manager1.MG5CanDraw1 = false;
            print("WrongDot");
        }
        else if(MG5Manager1.MG5CurrentGameobject1 != gameObject && MG5Manager1.MG5CurrentGameobject1 != null)
        {
            MG5LineDrawer2 CurrentLine = MG5Manager1.MG5CurrentGameobject1.GetComponent<MG5LineDrawer2>();
            CurrentLine.MG5_line.positionCount = 1;
            CurrentLine.MG5_line.SetPosition(0, MG5Manager1.MG5CurrentGameobject1.transform.position);
            MG5Manager1.MG5CanDraw1 = false;
            print("WrongDot");
        }
    }
}
