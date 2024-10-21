using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 spawnPosition; // Set this to the desired spawn position in the Inspector
    [Range(0,2)]
    public float spawnDelay = 0.5f; // Set this to the desired spawn delay in the Inspector

    void Start()
    {
        Invoke("SpawnPlayer", spawnDelay); // Delay the player spawn by 2 seconds
    }
    private void SpawnPlayer()
    {
        // Call the SpawnPlayer method from the GameManager
        GameManager.Instance.SpawnPlayer(spawnPosition);
    }
}