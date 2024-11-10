using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG33Manager : MonoBehaviour
{

    public GameObject[] dots;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y < -29)
        {
            AudioManager.instance.TakingNotesTheme();
        }
    }
}
