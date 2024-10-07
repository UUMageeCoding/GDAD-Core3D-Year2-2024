using UnityEngine;

// Base class Item with virtual DisplayInfo method
public class Item : MonoBehaviour
{
    protected string itemName;
    protected string description;

    // Default constructor
    public Item()
    {
        itemName = "Generic Item";
        description = "A generic item.";
        Debug.Log("1st Item Constructor Called");
    }

    // Constructor with parameters
    public Item(string newItemName, string newDescription)
    {
        itemName = newItemName;
        description = newDescription;
        Debug.Log("2nd Item Constructor Called");
    }

    // Virtual method to be overridden in derived classes
    public virtual void DisplayInfo()
    {
        Debug.Log($"{itemName}: {description}");
    }

    public void SayHello()
    {
        Debug.Log("Hello, I am an item.");
    }
}