using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MG5GM1 : MonoBehaviour
{

    public int MG51Part1Done, MG51Part2Done, MG51Part3Done;
    public bool isMG51Part1Done, isMG51Part2Done, isMG51Part3Done;
    public GameObject MG5text, MG5Part1, MG5Part2, MG5Part3, MG5Heart, MG5EndPanel;
    private SpriteRenderer MG5Panel;

    public Follower follower;

    private void Start()
    {
        Invoke("HidePart", 0.2f);
        MG5Panel = MG5EndPanel.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Part1();
        Part2();
        Part3();
        NextPart();
    }

    void Part1()
    {
        if(!isMG51Part1Done)
        {
            foreach (SpriteRenderer color in MG5Manager1.Part11)
            {
                if (color.color == Color.black)
                {
                    MG51Part1Done++;
                }
            }

            if (MG51Part1Done == 2)
            {
                print("Part1Done");
                isMG51Part1Done = true;
                MG5text.SetActive(true);
            }
            else
            {
                MG51Part1Done = 0;
            }
        }
    }

    void Part2()
    {
        if (!isMG51Part2Done)
        {
            foreach (SpriteRenderer color in MG5Manager1.Part22)
            {
                if (color.color == Color.black)
                {
                    MG51Part2Done++;
                }
            }

            if (MG51Part2Done == 2)
            {
                print("Part2Done");
                isMG51Part2Done = true;
                MG5text.SetActive(true);
            }
            else
            {
                MG51Part2Done = 0;
            }
        }
    }

    void Part3()
    {
        if (!isMG51Part3Done)
        {
            foreach (SpriteRenderer color in MG5Manager1.Part33)
            {
                if (color.color == Color.black)
                {
                    MG51Part3Done++;
                }
            }

            if (MG51Part3Done == 2)
            {
                print("Part3Done");
                isMG51Part3Done = true;
                MG5Heart.GetComponent<SpriteRenderer>().color = Color.red;
                MG5text.SetActive(true);
            }
            else
            {
                MG51Part3Done = 0;
            }
        }
        else
        {
            MG5text.SetActive(false);

            if (MG5Panel.color.a <= 1)
            {
                MG5Panel.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
            }
            else
            {
                follower.letsmove = true;
                print("true");
            }
        }
    }

    void NextPart()
    {
        if(Input.GetMouseButtonDown(1) && Camera.main.transform.position.y < -49f)
        {
            if(!isMG51Part2Done)
            {
                MG5Part1.SetActive(false);
                MG5Part2.SetActive(true);
                MG5text.SetActive(false);
            }
            else if (!isMG51Part3Done)
            {
                MG5Part2.SetActive(false);
                MG5Part3.SetActive(true);
                MG5Heart.SetActive(true);
                MG5text.SetActive(false);
            }
        }
    }

    void HidePart()
    {
        MG5Part1.SetActive(true);
        MG5Part2.SetActive(false);
        MG5Part3.SetActive(false);
        MG5text.SetActive(false);
        MG5Heart.SetActive(false);
        MG5Panel.color = new Color(1, 1, 1, 0);
    }
}
