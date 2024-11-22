
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform bulletSpawnPoint; // Reference to the bullet spawn point

    public float bulletSpeed = 20f; // Speed of the bullet
    public float shootCooldown = 0.1f; // Cooldown in seconds between shots

    private float lastShootTime = -100f; // Initialize to a low value

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
    }

    // Update is called once per frame
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
        // Instantiate the bullet prefab at the bullet spawn point position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Calculate the bullet's direction using the player's forward direction
        Vector3 bulletDirection = transform.forward; // Use the player's forward direction

        // Add a Rigidbody to the bullet and set its velocity
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = bulletDirection * bulletSpeed;
        }

        // Update the last shoot time to enforce cooldown
        lastShootTime = Time.time;
    }

    private void FireEffects(){
        //TODO - add a muzzle flash effect when shooting??
        
        //TODO - add a camera shake effect when shooting
        FeedbackEventManager.ShakeCamera(5f, 1f, 0.25f);
    }
}