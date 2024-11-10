using UnityEngine;
using System.Collections.Generic;

public class Touch_hand : MonoBehaviour
{

    public float targetSpeed = 0.5f; // Target average click speed (clicks per second)
    public float rotationSpeed = 100f; // Motor rotation speed when click speed is met
    public float timeWindow = 3f; // Time window for averaging click speed
    public float returnSpeed = 100f; // Speed at which objects return to the default angle when idle
    public float maxMotorTorque = 1000f; // Motor torque for rotation
    public float defaultAngleA = 0f; // Default return angle for object A
    public float defaultAngleB = 0f; // Default return angle for object B
    
    public GameObject objectA; // GameObject for the first arm
    public GameObject objectB; // GameObject for the second arm
    public GameObject newObjectToActivate; // GameObject to activate on collision


    private Queue<float> clickTimestamps = new Queue<float>();
    private bool objectsCollided = false;

    public Follower follower;

    private void Start()
    {

    }

    private void Update()
    {
        if (objectsCollided) return;

        // Check for mouse clicks and record timestamp
        if (Input.GetMouseButtonDown(0))
        {
            clickTimestamps.Enqueue(Time.time);
        }

        // Remove outdated timestamps
        while (clickTimestamps.Count > 0 && Time.time - clickTimestamps.Peek() > timeWindow)
        {
            clickTimestamps.Dequeue();
        }

        // Calculate average click speed
        float clickSpeed = clickTimestamps.Count / timeWindow;

        JointMotor2D motorA = hingeA.motor;
        JointMotor2D motorB = hingeB.motor;


        }
        else
        {
            // Return to default angles
            float angleDifferenceA = defaultAngleA - hingeA.jointAngle;
            float angleDifferenceB = defaultAngleB - hingeB.jointAngle;

            // Apply return speed towards the default angle
            motorA.motorSpeed = angleDifferenceA * returnSpeed;
            motorA.maxMotorTorque = maxMotorTorque;
            
            motorB.motorSpeed = angleDifferenceB * returnSpeed;
            motorB.maxMotorTorque = maxMotorTorque;
        }

        hingeA.motor = motorA;
        hingeB.motor = motorB;
    }

    public void HandleCollision()
    {
        if (!objectsCollided)
        {
            objectsCollided = true;

            // Destroy both objects
            Destroy(objectA);
            Destroy(objectB);

            // Activate the new object
            if (newObjectToActivate != null)
            {
                newObjectToActivate.SetActive(true);
                
                //End of Game Here
            }
        }
    }



}
