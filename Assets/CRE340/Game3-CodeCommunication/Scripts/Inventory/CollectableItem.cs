using UnityEngine;

public class CollectableItem : MonoBehaviour 
{
    public InventoryItem itemData; // Reference to the ScriptableObject

    private InventoryManager inventoryManager; // Reference to the InventoryManager - we will use this to add the item to the inventory
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) { // Ensure the player is the one collecting
            GameObject player = other.gameObject;
            if (player.GetComponent<InventoryManager>() != null) {
                inventoryManager = player.GetComponent<InventoryManager>(); // Get the InventoryManager reference
                
                // Check if there's room in the inventory
                if (inventoryManager.CanAddItem()) {
                    Collect(); // Collect the item
                } else {
                    Debug.Log("Cannot collect item, inventory is full");
                }
            }
        }
    }
    
    public void Collect() {
        inventoryManager.AddItem(itemData); // Add the item to the inventory
        Collected();
    }

    private void Collected() {
        Destroy(gameObject); // Optionally destroy the item in the scene after collection
    }
}