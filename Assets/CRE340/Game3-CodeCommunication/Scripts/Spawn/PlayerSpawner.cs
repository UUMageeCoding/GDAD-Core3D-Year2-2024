using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 spawnPosition; // Set this to the desired spawn position in the Inspector

    void Start()
    {
        // Call the SpawnPlayer method from the GameManager
        GameManager.Instance.SpawnPlayer(spawnPosition);
    }
}