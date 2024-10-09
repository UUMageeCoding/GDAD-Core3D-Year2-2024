using UnityEngine;

/// <summary>
/// This is a class for destroying objects using verios conditions
/// - destroy after a fixed time period
/// - destroy after a set number of collisions with another object
/// - destroy if the object is off screen - camera render viewport
/// - destroy if the object is idle for a fixed time - not moving
/// </summary>

public class ObjectDestruction : MonoBehaviour
{
    public bool destroyAfterTime = true;
    public float destructionTime = 5f;

    public bool destroyOnCollision = true;
    public int collisionDestroyThreshold = 2; // Number of collisions before destruction
    private int collisionCount;

    public bool destroyOffScreen = true;

    public bool destroyIfIdle = false;
    public float idleTimeThreshold = 3f;
    private float lastMoveTime;
    private Vector3 lastPosition;

    private Camera mainCamera;
    private bool isOffScreen = false;

    void Start()
    {
        mainCamera = Camera.main;
        lastPosition = transform.position;
        lastMoveTime = Time.time;

        if (destroyAfterTime)
        {
            Destroy(gameObject, destructionTime);
            //Debug.Log("Destroyed: " + gameObject.name + " after " + destructionTime + " seconds of time");
        }
    }

    void Update()
    {
        if (destroyOffScreen && !isOffScreen)
        {
            CheckOffScreen();
        }

        if (destroyIfIdle && (transform.position == lastPosition))
        {
            if (Time.time - lastMoveTime > idleTimeThreshold)
            {
                Destroy(gameObject);
                //Debug.Log("Destroyed: " + gameObject.name + " after " + idleTimeThreshold + " seconds of inactivity");
            }
        }
        else
        {
            lastPosition = transform.position;
            lastMoveTime = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount++;
        if (destroyOnCollision && collisionCount >= collisionDestroyThreshold)
        {
            Destroy(gameObject);
            //Debug.Log("Destroyed: " + gameObject.name + " after " + collisionCount + " collisions "+ "(" + collision.gameObject.name + ")");
        }
    }
    
    
    void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        if (destroyOnCollision && collisionCount >= collisionDestroyThreshold)
        {
            Destroy(gameObject);
            //Debug.Log("Destroyed: " + gameObject.name + " after " + collisionCount + " collisions "+ "(" + collision.gameObject.name + ")");
        }
    }

    void CheckOffScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (!onScreen)
        {
            isOffScreen = true;
            Destroy(gameObject);
            //Debug.Log("Destroyed: " + gameObject.name + " off screen");
        }
    }
}