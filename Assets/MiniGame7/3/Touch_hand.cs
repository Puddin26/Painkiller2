using System.Collections.Generic;
using UnityEngine;



public class Touch_hand : MonoBehaviour
{
    public Transform objectB, objectA;
    public GameObject handA, handB, handAA, handBB;
    public Vector3 initialPositionA;
    public Vector3 initialPositionB;
    public float targetSpeed = 0.5f; // Target average click speed (clicks per second)
    public float moveSpeed = 1f; // Speed at which objects move closer or farther
    public float timeWindow = 3f; // Time window for averaging click speed
    private Queue<float> clickTimestamps = new Queue<float>();
    private bool objectsCollided = false;
    public Follower follower;

    private void Start()
    {
        // Set initial positions from Inspector
        objectA.position = initialPositionA;
        objectB.position = initialPositionB;
        handA.SetActive(false);
        handB.SetActive(false);
    }

    private void Update()
    {

        if (objectsCollided) return;

        // Check for mouse clicks and record timestamp
        if (Input.GetMouseButtonDown(0))
        {
            clickTimestamps.Enqueue(Time.time);
            AudioManager.instance.TarotDialogue();
        }

        // Remove outdated clicks outside of the time window
        while (clickTimestamps.Count > 0 && Time.time - clickTimestamps.Peek() > timeWindow)
        {
            clickTimestamps.Dequeue();
        }

        // Calculate average click speed
        float averageClickSpeed = clickTimestamps.Count / timeWindow;

        // Move objects based on the average click speed
        if (averageClickSpeed >= targetSpeed)
        {
            // Move objects towards each other
            objectA.position = Vector3.MoveTowards(objectA.position, objectB.position, moveSpeed * Time.deltaTime);
            objectB.position = Vector3.MoveTowards(objectB.position, objectA.position, moveSpeed * Time.deltaTime);

            // Check if objects have collided
            if (Vector3.Distance(objectA.position, objectB.position) <= 0.01f)
            {
                objectsCollided = true;
                handA.SetActive(true);
                handB.SetActive(true);
                handAA.SetActive(false);
                handBB.SetActive(false);
                Debug.Log("Objects hit each other!");
                follower.nextPage = true;
            }
        }
        else
        {
            // Move objects back towards their initial positions
            objectA.position = Vector3.MoveTowards(objectA.position, initialPositionA, moveSpeed * Time.deltaTime);
            objectB.position = Vector3.MoveTowards(objectB.position, initialPositionB, moveSpeed * Time.deltaTime);
        }
    }
}
