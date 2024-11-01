using UnityEngine;
using DG.Tweening;

public class Enemy : EnemyBase
{
    public EnemyData enemyData;         // Reference to the EnemyData ScriptableObject
    public GameObject dieEffectPrefab;  // Reference to the die effect prefab
    private int health;                 // Enemy health
    public int damage = 10;             // Damage dealt by the enemy

    private void Awake()
    {
        // Apply the data from the ScriptableObject to the enemy
        gameObject.name = enemyData.enemyName;
        health = enemyData.health;
        damage = enemyData.damage;

        // Set initial color based on EnemyData
        GetComponent<Renderer>().material.color = enemyData.enemyColor;
    }

    private void OnEnable()
    {
        // Scale the enemy up from 0 to 1 over 1 second using DOTween for spawn animation
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        // Trigger the OnObjectDamaged event
        HealthEventManager.OnObjectDamaged?.Invoke(gameObject.name, health);

        // Show hit effect using the inherited ShowHitEffect method
        ShowHitEffect();

        if (health <= 0)
        {
            Die();
            // Trigger the OnObjectDestroyed event
            HealthEventManager.OnObjectDestroyed?.Invoke(gameObject.name, health);
        }
    }

    protected override void Die()
    {
        // Instantiate die effect and apply area damage
        if (dieEffectPrefab != null)
        {
            Instantiate(dieEffectPrefab, transform.position, Quaternion.identity);
        }

        // Play sound when the enemy dies
        AudioEventManager.PlaySFX(null, "Explosion Flesh", 1.0f, 1.0f, true, 0.1f, 0f);

        Destroy(gameObject); // Destroy the enemy GameObject
        Debug.Log("Enemy has died");

        // Increase the player's score based on enemy health
        GameManager.Instance.AddScore(10 * enemyData.health);
    }

    public override void Move()
    {
        // Define movement behaviour specific to this enemy type if needed
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the IDamagable interface
        IDamagable damagableObject = collision.gameObject.GetComponent<IDamagable>();
        
        // Prevent enemy from damaging other enemies
        if (damagableObject != null && collision.gameObject.tag != "Enemy")
        {
            damagableObject.TakeDamage(damage);
            Debug.Log($"{gameObject.name} dealt {damage} damage to {collision.gameObject.name}.");
        }
    }
}
