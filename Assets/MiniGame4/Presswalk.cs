using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presswalk : MonoBehaviour
{
    public MG4Gretchen gretch;

    // Start is called before the first frame update
    private void OnMouseDrag()
    {
        Camera.main.orthographic = false;
        if (!gretch.MG4_isLocked)
        {
            gretch.NPCs.transform.Translate(Vector3.back * gretch.MG4_walkSpeed * Time.deltaTime);

        }

    }
}
