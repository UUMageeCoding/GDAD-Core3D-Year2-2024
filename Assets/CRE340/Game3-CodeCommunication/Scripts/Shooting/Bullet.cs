using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    private Rigidbody rb;
    private BulletPool bulletPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        // Find the BulletPool manager
        bulletPool = FindObjectOfType<BulletPool>();
        
        // enable the bullet's stuff
        //this.gameObject.SetActive(true);
        GetComponent<Collider>().enabled = true;
        rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = true;

        // Disable the bullet's collider
        GetComponent<Collider>().enabled = false;

        // Check if the bullet hit something that has the 'IDamagable' interface
        if (collision.gameObject.GetComponent<IDamagable>() != null)
        {
            IDamagable damageable = collision.gameObject.GetComponent<IDamagable>();
            damageable.TakeDamage(damage);
            damageable.ShowHitEffect();

            // TODO - Add audio feedback when hitting an object
            AudioEventManager.PlaySFX(this.transform, "Slap Heavy", 1.0f, 1.0f, true, 0.1f, 0f, "null");
        }

        // Play a generic bullet hit sound
        AudioEventManager.PlaySFX(this.transform, "Dry Shot", 0.3f, 1.5f, true, 0.2f, 1f, "null");

        // Return the bullet to the pool or destroy it if pooling is not enabled
        StartCoroutine(WaitAndDestroy(0.5f));
        // if (bulletPool != null)
        // {
        //     bulletPool.ReturnBullet(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }
    
    private IEnumerator WaitAndDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        if (bulletPool != null)
        {
            bulletPool.ReturnBullet(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}