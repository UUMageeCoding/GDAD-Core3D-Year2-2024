using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    // Player reference (use your Player class)
    public GameObject playerPrefab;
    private Player playerInstance;

    // Reference to the UI_Display
    public UI_Display uiDisplay; // Set this in the Inspector

    // Private variables for game state
    private int _score = 0;

    // Public property for score
    public int Score
    {
        get { return _score; }
        set
        {
            _score = Mathf.Max(0, value); // Ensure score doesn't go below 0
            uiDisplay.UpdateScore(_score); // Update the score UI via UI_Display
        }
    }

    // Method to instantiate the player and keep track of its instance
    public void SpawnPlayer(Vector3 spawnPosition)
    {
        if (playerInstance == null) // Ensure we don't spawn multiple players
        {
            GameObject playerObject = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            playerInstance = playerObject.GetComponent<Player>();
        }

        // Update the UI for the initial health
        uiDisplay.UpdateHealth(playerInstance.health);
    }
    

    // Method to handle player taking damage
    public void PlayerHealth(int health)
    {
        if (playerInstance != null)
        {
            uiDisplay.UpdateHealth(health); // Update the health UI after damage
        }
    }

    private void Start()
    {
        // Initialize the UI with the current game state
        uiDisplay.UpdateScore(_score);
        if (playerInstance != null)
        {
            uiDisplay.UpdateHealth(playerInstance.health);
        }
    }
}
