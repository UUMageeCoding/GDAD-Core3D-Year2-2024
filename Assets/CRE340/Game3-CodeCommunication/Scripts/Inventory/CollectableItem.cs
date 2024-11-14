using UnityEngine;
using DG.Tweening;

public class CollectableItem : MonoBehaviour
{
    public InventoryItem itemData; // Reference to the ScriptableObject
    public GameObject collectEffectPrefab; // Prefab to instantiate on collection

    private InventoryManager inventoryManager; // Reference to the InventoryManager - we will use this to add the item to the inventory

    private void OnEnable()
    {
        // TODO - add an animation event to play the spawn animation tween
        //scale the item up from 0 to 1 in 1 second using DOTween
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(0.5f,0.5f,0.5f), 1f).SetEase(Ease.OutBounce);
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

        // Instantiate the collect effect prefab at the item's position
        if (collectEffectPrefab != null) {
            Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
        }

        //TODO - add and audio feedback when the object is collected
        AudioEventManager.PlaySFX(null, "Special Powerup",  1.0f, 1.0f, true, 0.1f, 0f, "null");

        Collected();
    }

    private void Collected() {
        Destroy(gameObject); // Optionally destroy the item in the scene after collection
    }
}