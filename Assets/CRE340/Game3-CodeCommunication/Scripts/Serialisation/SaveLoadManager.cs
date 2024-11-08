using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
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
    }

    public void SaveData()
    {
        // Convert the player data to JSON format
        string json = JsonUtility.ToJson(playerProperties, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved to " + filePath);
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

    // Example to modify player properties in-game (e.g., button triggers)
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
}