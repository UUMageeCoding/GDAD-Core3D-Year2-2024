
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
        //enable rigidbody gravity
        rb.useGravity = true;
        
        //check if the bullet hit something that has the 'IDamagable' interface
        if (collision.gameObject.GetComponent<IDamagable>() != null){
            IDamagable damageable = collision.gameObject.GetComponent<IDamagable>();
            damageable.TakeDamage(damage);
            damageable.ShowHitEffect();
        }

    }
}