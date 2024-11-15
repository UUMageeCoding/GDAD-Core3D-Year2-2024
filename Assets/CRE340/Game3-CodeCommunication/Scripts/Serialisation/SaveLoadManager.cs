// SaveLoadManager.cs
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [Header("Save and Load Options")]
    [Space(10)]
    public bool autoLoad; // Option to auto-load data
    public bool autoSave; // New public boolean option to auto-save data

    [Header("Player Properties to Save and Load")]
    [Space(10)]
    public PlayerProperties playerProperties;

    private string filePath;

    
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/playerData.json";

        // Initialize with default data if no existing data is loaded
        if (playerProperties == null)
        {
            playerProperties = new PlayerProperties();
        }
        
        //LoadData(); // Call the LoadData method to retrieve saved data from the file
        
    }
    
    public void LoadData()
    {

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerProperties = JsonUtility.FromJson<PlayerProperties>(json);
            Debug.Log("Data loaded from " + filePath);
        }
        else
        {
            Debug.LogWarning("Save file not found at " + filePath);
        }

    }
    public void SaveData()
    {
        // Convert the player data to JSON format
        string json = JsonUtility.ToJson(playerProperties, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved to " + filePath);
    }



    public void ClearData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Save data cleared from " + filePath);
        }
        else
        {
            Debug.LogWarning("No save file to delete at " + filePath);
        }

        // Reset player properties to default state
        playerProperties = new PlayerProperties();
    }

    // Example to modify player properties lists - not used currently
    public void AddToInventory(string item)
    {
        playerProperties.inventory.Add(item);
        Debug.Log(item + " added to inventory.");
    }

    public void GainExperience(int amount)
    {
        playerProperties.experience += amount;
        Debug.Log("Gained " + amount + " experience. Total: " + playerProperties.experience);
    }

    public void AddCoins(int amount)
    {
        playerProperties.coins += amount;
        Debug.Log("Gained " + amount + " coins. Total: " + playerProperties.coins);
    }

    public void SetPlayerName(string name)
    {
        playerProperties.name = name;
        Debug.Log("Player name set to " + name);
    }
    
}