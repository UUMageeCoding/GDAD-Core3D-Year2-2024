
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    
    private float speed;
    private Vector3 direction;
    
    private bool bulletFired = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;

        rb.useGravity = true;
        
        //check if the bullet hit something that has the 'IDamagable' interface   (Modify this script here to check if the object has the 'IDamagable' interface and call the 'TakeDamage' and ShowHitEffect method)


    }
}