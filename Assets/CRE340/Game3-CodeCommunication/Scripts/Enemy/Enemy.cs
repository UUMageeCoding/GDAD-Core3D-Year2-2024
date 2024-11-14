using UnityEngine;
using DG.Tweening;

public class Enemy : EnemyBase
{
    [Space(10)]  
    [Header("Enemy Data")]  
    public EnemyData enemyData; // Reference to the EnemyData ScriptableObject  
    [SerializeField] private int damage = 10; // Damage dealt by the enemy  
    [SerializeField] private int health = 10;  
    public float speed = 2f;  
    public float chaseRange = 5f;         // Range within which the enemy starts chasing  
    
    
    [Space(10)]  
    [Header("Enemy State")]  
    private IEnemyState currentState;     // Reference to the current state  
    public Transform target;              // Reference to the player or target  

    
    [Space(10)]  
    [Header("Enemy FX")]  
    public GameObject dieEffectPrefab; // Reference to the die effect prefab  

    private void Awake()
    {
        // Apply the data from the ScriptableObject to the enemy
        gameObject.name = enemyData.enemyName;
        health = enemyData.health;
        damage = enemyData.damage;
        speed = enemyData.speed;
        chaseRange = enemyData.chaseRange;

        // Set initial color based on EnemyData
        GetComponent<Renderer>().material.color = enemyData.enemyColor;
    }
    private void Start()
    {
        // Start with the Idle state
        SetState(new EnemyState_Idle());
        
        // Find the player in the scene
        Invoke("LocatePlayer", 1f);
    }
    private void OnEnable()
    {
        // Scale the enemy up from 0 to 1 over 1 second using DOTween for spawn animation
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
    }
    
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
    
    public string GetCurrentStateName()
    {
        if (currentState != null)
        {
            string stateName = currentState.GetType().Name;
            return stateName.Replace("Enemy", "");
        }
        return "No State";
    }

    
    //--------------------------------------------------------------------------------
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
        AudioEventManager.PlaySFX(null, "Explosion Flesh", 1.0f, 1.0f, true, 0.1f, 0f, "null");

        Destroy(gameObject); // Destroy the enemy GameObject
        Debug.Log("Enemy has died");

        // Increase the player's score based on enemy health
        GameManager.Instance.AddScore(4 * enemyData.health);
        
        // Increase the player's experience based on enemy health
        GameManager.Instance.AddExperience(1 * enemyData.health);
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
    
    private void LocatePlayer()
    {
        // Find the player in the scene if the player exaists
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }
}
