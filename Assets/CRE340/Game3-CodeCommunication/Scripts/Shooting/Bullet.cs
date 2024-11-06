
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
            
            //TODO - add an audio feedback when hitting an object
            AudioEventManager.PlaySFX(this.transform, "Slap Heavy",  1.0f, 1.0f, true, 0.1f, 0f, "null");
        }
        
        // if the bullet hits anything, send a sfx event to play the bullet hit sound
        AudioEventManager.PlaySFX(this.transform, "Dry Shot",  0.3f, 1.5f, true, 0.2f, 1f, "null");

    }
}