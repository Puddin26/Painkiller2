using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSearch : MonoBehaviour
{

    public Sprite applied;
    public Follower follower;
    public int jobs, job_total;

    public void Apply(GameObject button)
    {
        jobs++;
        button.GetComponent<Image>().sprite = applied;
        if (jobs == job_total)
        {
            follower.nextPage = true;
        }
    }
}
