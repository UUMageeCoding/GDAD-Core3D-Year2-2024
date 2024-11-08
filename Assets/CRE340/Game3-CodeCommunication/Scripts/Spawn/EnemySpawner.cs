using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyTypes;       // Array of enemy data types with their prefabs
    public Vector3 spawnArea;            // x, y, z (width, height, depth) of the spawn area
    public Vector3 playerCheckArea;      // x, y, z (width, height, depth) of the player check area
    public float startDelay = 1f;        // Delay before the first spawn
    public float minSpawnInterval = 2f;  // Minimum spawn interval (2 seconds)
    public float maxSpawnInterval = 5f;  // Maximum spawn interval (5 seconds)
    public int maxSpawnedObjects = 100;  // Maximum number of spawned objects
    public LayerMask playerLayer;        // Layer mask to identify the player

    private List<EnemyBase> spawnedEnemies = new List<EnemyBase>();

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(startDelay);

        // Repeatedly spawn enemies at random intervals
        while (spawnedEnemies.Count < maxSpawnedObjects)
        {
            if (IsPlayerInArea())
            {
                SpawnRandomEnemy();
            }
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private bool IsPlayerInArea()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, playerCheckArea / 2, Quaternion.identity, playerLayer);
        return colliders.Length > 0;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyTypes.Length == 0) return;  // Ensure there are enemy types to spawn

        // Pick a random enemy type from the array
        int randomIndex = Random.Range(0, enemyTypes.Length);
        EnemyData selectedEnemyData = enemyTypes[randomIndex];

        // Generate a random spawn position within the spawn area relative to the spawner's position
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(0, spawnArea.y),
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        ) + transform.position;

        // Use the factory to create the enemy
        EnemyBase enemy = EnemyFactory.CreateEnemy(selectedEnemyData, randomPosition);

        if (enemy != null)
        {
            spawnedEnemies.Add(enemy);
        }
    }

    // Method to visualize the spawn and player check areas in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnArea);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, playerCheckArea);
    }
}