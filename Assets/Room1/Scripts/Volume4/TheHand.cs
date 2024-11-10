using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TheHand : MonoBehaviour
{

    [SerializeField] GameObject part1;
    [SerializeField] GameObject part2;
    public TheEnd theEnd;

    private void Start()
    {
        part2.SetActive(false);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 5;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Gretchen_hand_0")
        {
            AudioManager.instance.LinkCon();
            part2.SetActive(true);
            part1.SetActive(false);
        }
        if (other.name == "Matthew_hand_0")
        {
            AudioManager.instance.LinkCon();
            theEnd.talk = true;
            part2.SetActive(true);
            part1.SetActive(false);
        }
    }
}
