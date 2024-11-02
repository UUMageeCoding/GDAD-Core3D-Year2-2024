using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyName;  // Name of the enemy
    public int health;        // Health value for the enemy
    public int damage;        // Damage value for the enemy
    public float speed;       // Movement speed of the enemy
    public float chaseRange;  // Range within which the enemy starts chasing
    public Color enemyColor;  // Color of the enemy
    public GameObject enemyPrefab;  // Reference to the specific prefab for this enemy
}

