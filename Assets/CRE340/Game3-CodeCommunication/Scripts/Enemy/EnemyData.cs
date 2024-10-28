using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;  // Name of the enemy
    public int health;        // Health value for the enemy
    public int damage;        // Damage value for the enemy
    public float speed;       // Movement speed of the enemy
    public Color enemyColor;  // Color of the enemy
}

