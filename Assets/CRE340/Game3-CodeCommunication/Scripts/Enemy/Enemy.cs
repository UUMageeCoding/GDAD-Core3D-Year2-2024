using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public EnemyData enemyData; // Reference to the EnemyData ScriptableObject
    public GameObject dieEffectPrefab; // Reference to the die effect prefab
    public int damage = 10; // Damage dealt by the enemy

    private int health = 10;

    private Material mat;
    private Color originalColor;

    private void Awake()
    {
        // Apply the data from the ScriptableObject to the enemy
        gameObject.name = enemyData.enemyName;
        health = enemyData.health;
        damage = enemyData.damage;
        GetComponent<Renderer>().material.color = enemyData.enemyColor;

        Debug.Log($"Enemy {enemyData.enemyName} spawned with {enemyData.health} health and {enemyData.speed} speed.");
    }

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;
    }

    // Method to handle taking damage (from player or other sources)
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Trigger the OnObjectDamaged event
        HealthEventManager.OnObjectDamaged?.Invoke(gameObject.name, health);

        ShowHitEffect();

        if (health <= 0)
        {
            Die();

            // Trigger the OnObjectDestroyed event
            HealthEventManager.OnObjectDestroyed?.Invoke(gameObject.name, health);
        }
    }

    private void Die()
    {
        // Instantiate die effect and apply area damage
        if (dieEffectPrefab != null)
        {
            Instantiate(dieEffectPrefab, transform.position, Quaternion.identity);
        }

        // Optional: add death logic, like spawning loot or playing an animation
        Destroy(gameObject);

        // Debug log to show that the enemy has died
        Debug.Log("Enemy has died");
        
        //increase the players score 
        GameManager.Instance.AddScore(10 * enemyData.health);
    }

    public void ShowHitEffect()
    {
        // Get the material and flash it red
        Material mat = GetComponent<Renderer>().material;
        mat.color = Color.red;
        Invoke("ResetMaterial", 0.1f);
    }

    private void ResetMaterial()
    {
        mat.color = originalColor;
    }

    // Method for the enemy to deal damage to another IDamagable object
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the IDamagable interface
        IDamagable damagableObject = collision.gameObject.GetComponent<IDamagable>();
        // Prevent enemy from damaging other enemies (check the tag or another distinguishing property)
        if (damagableObject != null && collision.gameObject.tag != "Enemy")
        {
            // Call TakeDamage on the object, dealing the enemy's damage amount
            damagableObject.TakeDamage(damage);
            Debug.Log($"{gameObject.name} dealt {damage} damage to {collision.gameObject.name}.");
        }
    }
}
