using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG5GM : MonoBehaviour
{

    public int MG5Part1Done, MG5Part2Done, MG5Part3Done;
    public bool isMG5Part1Done, isMG5Part2Done, isMG5Part3Done;
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
        if(!isMG5Part1Done)
        {
            foreach (SpriteRenderer color in MG5Manager.Part1)
            {
                if (color.color == Color.black)
                {
                    MG5Part1Done++;
                }
            }

            if (MG5Part1Done == 10)
            {
                print("Part1Done");
                isMG5Part1Done = true;
                MG5text.SetActive(true);
            }
            else
            {
                MG5Part1Done = 0;
            }
        }
    }

    void Part2()
    {
        if (!isMG5Part2Done)
        {
            foreach (SpriteRenderer color in MG5Manager.Part2)
            {
                if (color.color == Color.black)
                {
                    MG5Part2Done++;
                }
            }

            if (MG5Part2Done == 4)
            {
                print("Part2Done");
                isMG5Part2Done = true;
                MG5text.SetActive(true);
            }
            else
            {
                MG5Part2Done = 0;
            }
        }
    }

    void Part3()
    {
        if (!isMG5Part3Done)
        {
            foreach (SpriteRenderer color in MG5Manager.Part3)
            {
                if (color.color == Color.black)
                {
                    MG5Part3Done++;
                }
            }

            if (MG5Part3Done == 2)
            {
                print("Part3Done");
                isMG5Part3Done = true;
                MG5Heart.GetComponent<SpriteRenderer>().color = Color.red;
                MG5text.SetActive(true);
            }
            else
            {
                MG5Part3Done = 0;
            }
        }
        else
        {
            if (MG5Panel.color.a <= 1)
            {
                MG5Panel.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
            }
            else
            {
                follower.letsmove = true;
            }
        }
    }

    void NextPart()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(!isMG5Part2Done)
            {
                MG5Part1.SetActive(false);
                MG5Part2.SetActive(true);
                MG5text.SetActive(false);
            }
            else if (!isMG5Part3Done)
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
