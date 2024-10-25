using UnityEngine;
using System.Collections.Generic;

public class Touch_hand : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;
    public Vector3 initialPositionA;
    public Vector3 initialPositionB;
    public float targetSpeed = 0.5f; // Target average click speed (clicks per second)
    public float moveSpeed = 1f; // Speed at which objects move closer or farther
    public float timeWindow = 3f; // Time window for averaging click speed

    private Queue<float> clickTimestamps = new Queue<float>();
    private bool objectsCollided = false;

    private void Start()
    {
        // Set initial positions from Inspector
        objectA.position = initialPositionA;
        objectB.position = initialPositionB;
    }

    private void Update()
    {
        if (objectsCollided) return;

        // Check for mouse clicks and record timestamp
        if (Input.GetMouseButtonDown(0))
        {
            clickTimestamps.Enqueue(Time.time);
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
                Debug.Log("Objects hit each other!");
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
