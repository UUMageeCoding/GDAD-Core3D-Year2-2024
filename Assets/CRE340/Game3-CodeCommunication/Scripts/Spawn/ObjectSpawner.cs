using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random; // Import for using Lists

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;  // Array of prefabs to spawn
    
    public Vector3 spawnArea;           // x, y, z (width, height, depth) of the spawn area
    public float minSpawnInterval = 1f; // Minimum spawn interval (2 seconds)
    public float maxSpawnInterval = 3f; // Maximum spawn interval (5 seconds)
    

    void Start()
    {
        // Start invoking the SpawnObject method at a random interval
        InvokeRepeating("SpawnRandomObject", Random.Range(minSpawnInterval, maxSpawnInterval), Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void SpawnRandomObject()
    {
        if (objectPrefab==null) return;  // Ensure there is something to spawn

        // Pick a random prefab from the array
        GameObject prefabToSpawn = objectPrefab;

        // Generate a random position within the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(0, spawnArea.y),
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        // Instantiate the prefab at the random position
        GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        

        // Reschedule the next spawn with a new random interval
        CancelInvoke("SpawnRandomObject"); // Cancel the current schedule
        Invoke("SpawnRandomObject", Random.Range(minSpawnInterval, maxSpawnInterval)); // Schedule the next spawn
    }

    // Method to visualize the spawn area in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
    
}
