using UnityEngine;

using DG.Tweening;
using UnityEditor;

public class Enemy : MonoBehaviour, IDamagable
{
<<<<<<< Updated upstream
    public EnemyData enemyData; // Reference to the EnemyData ScriptableObject
    public GameObject dieEffectPrefab; // Reference to the die effect prefab
    public int damage = 10; // Damage dealt by the enemy

    private int health = 10;

    private Material mat;
    private Color originalColor;

=======
    [Space(10)]  
    [Header("Enemy Data")]  
    public EnemyData enemyData; // Reference to the EnemyData ScriptableObject  
    private int damage = 10; // Damage dealt by the enemy  
    private int health = 10;  
    private float speed = 2f;
    
    
    [Space(10)]  
    [Header("Enemy State")]  
    private IEnemyState currentState;     // Reference to the current state  
    public Transform target;              // Reference to the player or target  
    public float chaseRange = 5f;         // Range within which the enemy starts chasing  
    public float chaseSpeed = 3f;         // Speed of the enemy while chasing  
  
    
    [Space(10)]  
    [Header("Enemy FX")]  
    public GameObject dieEffectPrefab; // Reference to the die effect prefab  
    // private Material mat;  
    // private Color originalColor;  
    
    
>>>>>>> Stashed changes
    private void Awake()
    {
        // Apply data from EnemyData to set properties
        gameObject.name = enemyData.enemyName;
        health = enemyData.health;
        damage = enemyData.damage;
<<<<<<< Updated upstream
=======
        // speed is not set here - 

        // Set initial colour based on EnemyData
>>>>>>> Stashed changes
        GetComponent<Renderer>().material.color = enemyData.enemyColor;

        Debug.Log($"Enemy {enemyData.enemyName} spawned with {enemyData.health} health and {enemyData.speed} speed.");
    }

    private void Start()
    {
<<<<<<< Updated upstream
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;
=======
        // Start with the Idle state
        SetState(new State_Idle());
        
        // Find the player in the scene
        Invoke("LocatePlayer", 1f);
>>>>>>> Stashed changes
    }
    
    private void OnEnable()
    {
<<<<<<< Updated upstream
        // TODO - add an animation event to play the spawn animation tween
        //scale the enemy up from 0 to 1 in 1 second using DOTween
=======
        // Spawn animation: Scale from 0 to 1 over 1 second with DOTween
>>>>>>> Stashed changes
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
        
    }

<<<<<<< Updated upstream
    // Method to handle taking damage (from player or other sources)
    public void TakeDamage(int damage)
=======
    private void Update()
    {
        // Delegate behaviour to the current state
        currentState?.Update(this);
    }
    
    public void SetState(IEnemyState newState)
    {
        // Exit the current state and enter the new state
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }
    
    //--------------------------------------------------------------------------------
    
    public override void TakeDamage(int damage)
>>>>>>> Stashed changes
    {
        health -= damage;

        // Trigger damage event and inherited hit effect
        HealthEventManager.OnObjectDamaged?.Invoke(gameObject.name, health);
<<<<<<< Updated upstream

        ShowHitEffect();
=======
        ShowHitEffect();  // Inherited from EnemyBase
>>>>>>> Stashed changes

        if (health <= 0)
        {
            Die();
<<<<<<< Updated upstream

            // Trigger the OnObjectDestroyed event
=======
>>>>>>> Stashed changes
            HealthEventManager.OnObjectDestroyed?.Invoke(gameObject.name, health);
        }
    }

    private void Die()
    {
        // Play death effect and sound
        if (dieEffectPrefab != null)
        {
            Instantiate(dieEffectPrefab, transform.position, Quaternion.identity);
        }
        
        //TODO - add and audio feedback when the enemy dies
        AudioEventManager.PlaySFX(null, "Explosion Flesh",  1.0f, 1.0f, true, 0.1f, 0f);

<<<<<<< Updated upstream
        // Optional: add death logic, like spawning loot or playing an animation
        Destroy(gameObject);

        // Debug log to show that the enemy has died
        Debug.Log("Enemy has died");
        
        //increase the players score 
=======
        AudioEventManager.PlaySFX(null, "Explosion Flesh", 1.0f, 1.0f, true, 0.1f, 0f);
        Destroy(gameObject);
        Debug.Log("Enemy has died");

        // Update score based on enemy health
>>>>>>> Stashed changes
        GameManager.Instance.AddScore(10 * enemyData.health);
    }

    public void ShowHitEffect()
    {
<<<<<<< Updated upstream
        // Get the material and flash it red
        Material mat = GetComponent<Renderer>().material;
        mat.color = Color.red;
        Invoke("ResetMaterial", 0.1f);
        
        //TODO - add an audio feedback when the enemy is hit
        AudioEventManager.PlaySFX(this.transform, "Flesh Hit",  1.0f, 1.0f, true, 0.1f, 0f);
=======
        // Define movement specific to this enemy, if needed
>>>>>>> Stashed changes
    }

    private void ResetMaterial()
    {
        mat.color = originalColor;
    }

    // Method for the enemy to deal damage to another IDamagable object
    private void OnCollisionEnter(Collision collision)
    {
        // Apply damage to other objects implementing IDamagable
        IDamagable damagableObject = collision.gameObject.GetComponent<IDamagable>();
<<<<<<< Updated upstream
        // Prevent enemy from damaging other enemies (check the tag or another distinguishing property)
=======

>>>>>>> Stashed changes
        if (damagableObject != null && collision.gameObject.tag != "Enemy")
        {
            // Call TakeDamage on the object, dealing the enemy's damage amount
            damagableObject.TakeDamage(damage);
            Debug.Log($"{gameObject.name} dealt {damage} damage to {collision.gameObject.name}.");
        }
    }
    
    private void LocatePlayer()
    {
        // Find the player in the scene if the player exaists
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }
}