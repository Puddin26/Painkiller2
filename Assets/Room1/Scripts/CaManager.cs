using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaManager : MonoBehaviour
{
    [SerializeField] int total;
    public int numnum;
    public bool donedone;

    public Follower follower;

    private void Update()
    {
        if(total == numnum && !donedone)
        {
            follower.letsmove = true;
            donedone = true;
        }
    }

}
