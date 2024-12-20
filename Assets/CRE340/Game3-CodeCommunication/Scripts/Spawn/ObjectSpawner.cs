using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;  // Array of prefabs to spawn
    public Vector3 spawnArea;           // x, y, z (width, height, depth) of the spawn area
    public Vector3 playerCheckArea;     // x, y, z (width, height, depth) of the player check area
    public float startDelay = 1f;       // Delay before the first spawn
    public float minSpawnInterval = 2f; // Minimum spawn interval (2 seconds)
    public float maxSpawnInterval = 5f; // Maximum spawn interval (5 seconds)
    public int maxSpawnedObjects = 10;  // Maximum number of spawned objects
    public LayerMask playerLayer;       // Layer mask to identify the player

    // List to store references to all spawned objects
    public List<GameObject> spawnedObjects = new List<GameObject>();

    // Event to notify when the object is spawned
    public static event Action<GameObject, float> OnObjectSpawned;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    private void Update()
    {
        // On press of 'O' key, show the number of spawned objects - for testing purposes
        if (Input.GetKeyDown(KeyCode.O))
        {
            ShowSpawnedObjectsCount();
        }
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(startDelay);
        // Start invoking the SpawnObject method at a random interval
        InvokeRepeating("SpawnRandomObject", Random.Range(minSpawnInterval, maxSpawnInterval), Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    private bool IsPlayerInArea()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, playerCheckArea / 2, Quaternion.identity, playerLayer);
        return colliders.Length > 0;
    }

    void SpawnRandomObject()
    {
        if (objectPrefabs.Length == 0) return;  // Ensure there is something to spawn

        // Check if the number of spawned objects has reached the limit
        if (spawnedObjects.Count >= maxSpawnedObjects) return;

        // Check if the player is in the defined area
        if (!IsPlayerInArea()) return;

        // Pick a random prefab from the array
        int randomIndex = Random.Range(0, objectPrefabs.Length);
        GameObject prefabToSpawn = objectPrefabs[randomIndex];

        // Generate a random position within the spawn area relative to the spawner's position
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(0, spawnArea.y),
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        ) + transform.position;

        // Instantiate the prefab at the random position
        GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);

        // Add the newly spawned object to the list
        spawnedObjects.Add(spawnedObject);

        // Reschedule the next spawn with a new random interval
        CancelInvoke("SpawnRandomObject"); // Cancel the current schedule
        Invoke("SpawnRandomObject", Random.Range(minSpawnInterval, maxSpawnInterval)); // Schedule the next spawn
    }

    // Method to visualize the spawn and player check areas in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnArea);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, playerCheckArea);
    }

    // For teaching purposes, you could add a method to access or manipulate the list, like this:
    public void ShowSpawnedObjectsCount()
    {
        Debug.Log("Number of spawned objects: " + spawnedObjects.Count);
    }
}