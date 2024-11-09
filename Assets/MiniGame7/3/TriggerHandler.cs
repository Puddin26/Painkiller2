using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public Touch_hand manager; // Reference to the manager script

    private void OnTriggerEnter2D(Collider2D other)
    {
        manager.HandleCollision(); // Call the collision handling method on the manager
    }
}