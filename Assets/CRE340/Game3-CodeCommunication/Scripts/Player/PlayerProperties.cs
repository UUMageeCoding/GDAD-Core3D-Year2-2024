using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProperties
{
    public int level;
    public int health;
    public int experience;
    public int coins;
    public List<string> inventory;

    public PlayerProperties(int level, int health, int experience, int coins, List<string> inventory)
    {
        this.level = level;
        this.health = health;
        this.experience = experience;
        this.coins = coins;
        this.inventory = inventory;
    }

    // Default constructor for empty player data
    public PlayerProperties()
    {
        level = 1;
        health = 100;
        experience = 0;
        coins = 0;
        inventory = new List<string>();
    }
}