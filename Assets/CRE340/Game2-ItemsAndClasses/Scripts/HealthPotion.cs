using UnityEngine;

// Derived class HealthPotion that overrides DisplayInfo
public class HealthPotion : Item
{
    public int healthRestoreAmount;

    // Default constructor
    public HealthPotion()
    {
        itemName = "Health Potion";
        description = "A potion that restores health.";
        healthRestoreAmount = 50;
        Debug.Log("1st HealthPotion Constructor Called");
    }

    // Constructor with parameters using 'base'
    public HealthPotion(string newItemName, string newDescription, int newHealthAmount) : base(newItemName, newDescription)
    {
        healthRestoreAmount = newHealthAmount;
        Debug.Log("2nd HealthPotion Constructor Called");
    }

    // Override method to display specific health potion info
    public override void DisplayInfo()
    {
        Debug.Log($"{itemName}: Restores {healthRestoreAmount} health points.");
    }
}