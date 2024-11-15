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
        if (saveLoadManager == null)
        {
            Debug.LogError("SaveLoadManager not found in the scene.");
        }
        else
        {
            if (saveLoadManager.autoLoad){
                LoadData();
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (saveLoadManager.autoSave){
            saveLoadManager.SaveData();
        }
    }

    private void OnDisable()
    {
        if (saveLoadManager.autoSave){
            saveLoadManager.SaveData();
        }
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
        UIEventHandler.ExperienceChanged(experience);
        saveLoadManager.playerProperties.experience = experience;
        if (saveLoadManager.autoSave){
            saveLoadManager.SaveData();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UIEventHandler.CoinsChanged(coins);
        saveLoadManager.playerProperties.coins = coins;
        if(saveLoadManager.autoSave){
            saveLoadManager.SaveData();
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion 
    
    public void ClearData()
    {
        // Reset GameManager properties
        ResetData();
        
        saveLoadManager.ClearData();
        
        // Notify UI about the changes
        UpdateUI();
    }

    public void LoadData()
    {
        saveLoadManager.LoadData();
        
        //get the loaded data
        GetSaveData();
        
        // Update UI with loaded data
        UpdateUI();
    }

    public void SaveData(){
        saveLoadManager.SaveData();
        
        //get the loaded data
        GetSaveData();
        
        // Update UI with loaded data
        UpdateUI();
    }
    
    private void ResetData(){
        playerName = "Player1";
        playerHealth = 100;
        score = 0;
        experience = 0;
        coins = 0;
    }
    private void GetSaveData(){
        playerName = saveLoadManager.playerProperties.name;
        playerHealth = 100;
        score = 0;
        experience = saveLoadManager.playerProperties.experience;
        coins = saveLoadManager.playerProperties.coins;
    }
    private void UpdateUI(){
        UIEventHandler.PlayerNameChanged(playerName);
        UIEventHandler.PlayerHealthChanged(playerHealth);
        UIEventHandler.ScoreChanged(score);
        UIEventHandler.ExperienceChanged(experience);
        UIEventHandler.CoinsChanged(coins);
    }

    
    
    

}