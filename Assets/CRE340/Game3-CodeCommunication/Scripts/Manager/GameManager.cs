using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Implementation

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

    public GameObject playerPrefab;
    private Player playerInstance;
    private SaveLoadManager saveLoadManager;

    [SerializeField] private string playerName = "Player1";
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private int score = 0;
    [SerializeField] private int experience = 0;
    [SerializeField] private int coins = 0;

    public string PlayerName
    {
        get { return playerName; }
        private set
        {
            playerName = value;
            UIEventHandler.PlayerNameChanged(playerName);
            saveLoadManager.SetPlayerName(playerName);
        }
    }

    public int PlayerHealth
    {
        get { return playerHealth; }
        private set
        {
            playerHealth = value;
            UIEventHandler.PlayerHealthChanged(playerHealth);
        }
    }

    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            UIEventHandler.ScoreChanged(score);
        }
    }

    public int Experience
    {
        get { return experience; }
        private set
        {
            experience = value;
            UIEventHandler.ExperienceChanged(experience);
            saveLoadManager.GainExperience(experience);
        }
    }

    public int Coins
    {
        get { return coins; }
        private set
        {
            coins = value;
            UIEventHandler.CoinsChanged(coins);
            saveLoadManager.AddCoins(coins);
        }
    }

    #endregion

    #region Unity Methods

    private void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        saveLoadManager.LoadData();
        Debug.Log("GameManager initialized with default player state:");
    }

    private void OnApplicationQuit()
    {
        saveLoadManager.SaveData();
    }

    #endregion

    #region Custom Public Methods

    public void SpawnPlayer(Vector3 spawnPosition)
    {
        if (playerInstance == null)
        {
            GameObject playerObject = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            playerInstance = playerObject.GetComponent<Player>();
            SetPlayerName(playerInstance.name);
            SetPlayerHealth(playerInstance.health);
        }
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void SetPlayerHealth(int health)
    {
        PlayerHealth = Mathf.Clamp(health, 0, 100);
        if (PlayerHealth <= 0)
        {
            Invoke("RestartLevel", 5F);
        }
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public void AddExperience(int points)
    {
        Experience += points;
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}