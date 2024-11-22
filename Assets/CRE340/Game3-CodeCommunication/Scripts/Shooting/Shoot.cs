using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform bulletSpawnPoint; // Reference to the bullet spawn point

    public float bulletSpeed = 20f; // Speed of the bullet
    public float shootCooldown = 0.1f; // Cooldown in seconds between shots

    private float lastShootTime = -100f; // Initialize to a low value
    private BulletPool bulletPool; // Reference to the bullet pool manager

    public bool useObjectPooling = true; // Toggle for object pooling

    void Start()
    {
        // If no bullet spawn point is assigned, create a new one
        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = new GameObject().transform;
            bulletSpawnPoint.name = "Bullet Spawn Point";
            bulletSpawnPoint.parent = transform; // Set it as a child of the player
            bulletSpawnPoint.position = transform.position + transform.forward + new Vector3(0, 0.2f, 0); // Slightly in front of the player
        }

        // Find the BulletPool component
        bulletPool = FindObjectOfType<BulletPool>();
    }

    void Update()
    {
        // Check for spacebar input and shoot if cooldown has elapsed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShootTime + shootCooldown)
        {
            Fire();
            FireEffects();
        }
    }

    void Fire()
    {
        GameObject bullet;

        if (useObjectPooling && bulletPool != null)
        {
            // Use object pooling
            bullet = bulletPool.GetBullet();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
            bullet.transform.parent = bulletPool.transform; // Set the pool as the parent
        }
        else
        {
            // Non-object pooling fallback
            bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }

        // Set bullet velocity
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = transform.forward * bulletSpeed;
        }

        // Update the last shoot time to enforce cooldown
        lastShootTime = Time.time;
    }

    private void FireEffects()
    {
        // TODO - Add a muzzle flash effect when shooting
        
        FeedbackEventManager.ShakeCamera(5f, 1f, 0.25f);
    }
}
