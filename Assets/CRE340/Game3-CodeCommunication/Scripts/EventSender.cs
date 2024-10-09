using UnityEngine;

public class EventSender : MonoBehaviour
{
    // Define the delegate type for the OnFire event
    public delegate void FireEventHandler(float scale, float speed);
    // Define the event based on the delegate
    public static event FireEventHandler OnFire;

    [Header("Parameters to pass with the event")]
    public float scale = 2.0f;
    public float speed = 10.0f;
    
    private void Update()
    {
        // Check for space bar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If there are any subscribers, invoke the event and pass parameters
            if (OnFire != null){
                OnFire(scale, speed); // Example: scale=2.0, speed=3.0
            }
        }
    }
}