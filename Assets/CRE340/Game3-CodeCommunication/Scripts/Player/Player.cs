using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public string playerName; // Name of the player
    public int health = 100; // Player health
    public GameObject dieEffectPrefab; // Reference to the die effect prefab

    private Material mat;
    private Color originalColor;

    private void Awake()
    {
        // Set the player name and initialize player stats
        gameObject.name = playerName;
        Debug.Log($"Player {playerName} spawned with {health} health.");
    }

    private void Start()
    {
        // Initialize material and original color
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;
    }

    public void TakeDamage(int damage)
    {
        // Reduce health by damage amount
        health -= damage;

        // Trigger the OnObjectDamaged event (optional)
        HealthEventManager.OnObjectDamaged?.Invoke(gameObject.name, health);
        
        //update the player health UI
        GameManager.Instance.SetPlayerHealth(health);

        ShowHitEffect();

        // Check if the player has died
        if (health <= 0)
        {
            health = 0;
            Die();

            // Trigger the OnObjectDestroyed event (optional)
            HealthEventManager.OnObjectDestroyed?.Invoke(gameObject.name, health);
        }
    }

    private void Die()
    {
        // Instantiate the die effect at the player's position
        if (dieEffectPrefab != null)
        {
            Instantiate(dieEffectPrefab, transform.position, Quaternion.identity);
        }

        // Optional: Add any additional death logic (e.g., respawn, game over)
       
        //Destroy(gameObject);
        
        //disable the movement and shooting scripts and render the player invisible
        GetComponent<FixedCameraMovementController>().enabled = false;
        GetComponent<Shoot>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        Debug.Log($"Player {playerName} has died.");
    }

    public void ShowHitEffect()
    {
        // Flash the player material red on hit
        mat.color = Color.red;
        Invoke("ResetMaterial", 0.1f);
    }

    private void ResetMaterial()
    {
        // Reset the player material to the original color
        mat.color = originalColor;
    }
}