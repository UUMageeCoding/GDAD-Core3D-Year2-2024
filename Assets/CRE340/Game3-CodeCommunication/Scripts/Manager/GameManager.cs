using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Implementation
    
    // Singleton instance
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }

    #endregion
    
    

    #region Properties and Fields
    
    // Player reference (use your Player class)
    public GameObject playerPrefab;
    private Player playerInstance;

    // Private backing fields for inspector visibility
    [SerializeField] private string playerName = "Player1"; // Default player name
    [SerializeField] private int playerHealth = 100;        // Default health
    [SerializeField] private int score = 0;                 // Default score

    // Public properties to access these fields but prevent external modification
    public string PlayerName
    {
        get { return playerName; }
        private set
        {
            playerName = value;
            UIEventHandler.PlayerNameChanged(playerName); // Notify listeners
        }
    }

    public int PlayerHealth
    {
        get { return playerHealth; }
        private set
        {
            playerHealth = value;
            UIEventHandler.PlayerHealthChanged(playerHealth); // Notify listeners
        }
    }

    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            UIEventHandler.ScoreChanged(score); // Notify listeners
        }
    }
    #endregion



    #region Unity Methods
    private void Start()
    {
        // Initialize with default values (optional for later use)
        Debug.Log("GameManager initialized with default player state:");
    }
    #endregion


    #region Custom Public Methods
    
    // Method to instantiate the player and keep track of its instance
    public void SpawnPlayer(Vector3 spawnPosition)
    {
        if (playerInstance == null) // Ensure we don't spawn multiple players
        {
            GameObject playerObject = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            playerInstance = playerObject.GetComponent<Player>();
            SetPlayerName(playerInstance.name);
            SetPlayerHealth(playerInstance.health);
        }
    }

    // Method to set the player name
    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    // Method to update player health
    public void SetPlayerHealth(int health)
    {
        PlayerHealth = Mathf.Clamp(health, 0, 100); // Ensure health stays between 0 and 100
        if (PlayerHealth <= 0)
        {
            // Handle player death, such as restarting level or showing game over
            Invoke("RestartLevel", 5F);
        }
    }

    // Method to increase the score
    public void AddScore(int points)
    {
        Score += points;
    }

    // Method to restart the current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    #endregion

}
