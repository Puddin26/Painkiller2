using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG4GameManager : MonoBehaviour
{
    public float MG4_walkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) { gameObject.transform.Translate(Vector3.forward * MG4_walkSpeed * Time.deltaTime); }
        if(Input.GetKey(KeyCode.S)) { gameObject.transform.Translate(Vector3.back * MG4_walkSpeed * Time.deltaTime); }
    }
}
