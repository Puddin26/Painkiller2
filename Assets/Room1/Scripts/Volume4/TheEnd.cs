using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour
{
    public GameObject[] set1;
    private int randomNumber;
    private int randomNumber2;
    public bool talk;
    [SerializeField] Sprite final;
    public Follower follower;

    private void Start()
    {
        foreach (var item in set1)
        {
            item.gameObject.SetActive(false);
        }

        InvokeRepeating("Endgame", 2, 2);
    }


    public void Endgame()
    {
        if(talk)
        {
            follower.letsmove = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = final;

            randomNumber = Random.Range(0, set1.Length - 1);
            randomNumber2 = Random.Range(0, set1.Length - 1);
            if (randomNumber == randomNumber2)
            {
                randomNumber2 = Random.Range(0, set1.Length - 1);
            }
            if (randomNumber == randomNumber2)
            {
                randomNumber2 = Random.Range(0, set1.Length - 1);
            }

            set1[randomNumber].SetActive(true);
            set1[randomNumber2].SetActive(true);
        }
    }

}
