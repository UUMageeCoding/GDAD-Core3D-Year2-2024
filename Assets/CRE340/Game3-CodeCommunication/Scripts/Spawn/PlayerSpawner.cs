using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 spawnOffset; // Set this to the desired spawn offset in the Inspector
    [Range(0, 2)]
    public float spawnDelay = 0.5f; // Set this to the desired spawn delay in the Inspector

    void Start()
    {
        Invoke("SpawnPlayer", spawnDelay); // Delay the player spawn by the specified seconds
    }

    private void SpawnPlayer()
    {
        // Calculate the spawn position relative to the spawner's position
        Vector3 spawnPosition = transform.position + spawnOffset;

        // Call the SpawnPlayer method from the GameManager
        GameManager.Instance.SpawnPlayer(spawnPosition);
    }
}