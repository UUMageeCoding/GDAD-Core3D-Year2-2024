using UnityEngine;

// Derived class ManaPotion that overrides DisplayInfo
public class ManaPotion : Item
{
    public int manaRestoreAmount;

    // Default constructor
    public ManaPotion()
    {
        itemName = "Mana Potion";
        description = "A potion that restores mana.";
        manaRestoreAmount = 30;
        Debug.Log("1st ManaPotion Constructor Called");
    }

    // Constructor with parameters using 'base'
    public ManaPotion(string newItemName, string newDescription, int newManaAmount) : base(newItemName, newDescription)
    {
        manaRestoreAmount = newManaAmount;
        Debug.Log("2nd ManaPotion Constructor Called");
    }

    // Override method to display specific mana potion info
    public override void DisplayInfo()
    {
        Debug.Log($"{itemName}: Restores {manaRestoreAmount} mana points.");
    }
}