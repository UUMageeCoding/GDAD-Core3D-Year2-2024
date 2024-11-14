using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProperties
{
    public string name;
    public int health;
    public int experience;
    public int coins;
    public List<string> inventory;

    public PlayerProperties(string name, int experience, int coins, List<string> inventory)
    {
        this.name = name;
        this.experience = experience;
        this.coins = coins;
        this.inventory = inventory;
    }

    // Default constructor for empty player data
    public PlayerProperties()
    {
        name = "Player";
        health = 100;
        experience = 0;
        coins = 0;
        inventory = new List<string>();
    }
}