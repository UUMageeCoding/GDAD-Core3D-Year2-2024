using UnityEngine;
using DG.Tweening;

public class CollectableItem : MonoBehaviour 
{
    public InventoryItem itemData; // Reference to the ScriptableObject

    private InventoryManager inventoryManager; // Reference to the InventoryManager - we will use this to add the item to the inventory
    
    private void OnEnable()
    {
        // TODO - add an animation event to play the spawn animation tween
        //scale the item up from 0 to 1 in 1 second using DOTween
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
        
    }
    
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
        
        //TODO - add and audio feedback when the object is collected
        AudioEventManager.PlaySFX(null, "Special Powerup",  1.0f, 1.0f, true, 0.1f, 0f, "null");
        
        Collected();
    }

    private void Collected() {
        Destroy(gameObject); // Optionally destroy the item in the scene after collection
    }
}