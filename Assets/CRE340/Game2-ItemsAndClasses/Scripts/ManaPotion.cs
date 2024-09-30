using UnityEngine;

public class ManaPotion : Item
{
    public int manaRestoreAmount;
    public int minRestoreAmount = 20; // Minimum restore amount for mana
    public int maxRestoreAmount = 50; // Maximum restore amount for mana

    // Default constructor
    public ManaPotion()
    {
        itemName = "Mana Potion";
        description = "A potion that restores mana.";
    }

    // Called when the object is instantiated
    private void Start()
    {
        // Assign a random value for manaRestoreAmount within the specified range
        manaRestoreAmount = Random.Range(minRestoreAmount, maxRestoreAmount);
        Debug.Log($"ManaPotion: Random restore amount set to {manaRestoreAmount}.");
    }

    // Override method to display specific mana potion info
    public override void DisplayInfo()
    {
        Debug.Log($"{itemName}: Restores {manaRestoreAmount} mana points. (ManaPotion Class DisplayInfo Called)");
    }
}