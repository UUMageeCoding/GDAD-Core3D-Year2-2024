using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour, ICollectable {
    public InventoryItem itemData; // Reference to the ScriptableObject

    public void Collect() {
        //InventoryManager.Instance.AddItem(itemData);
        Destroy(gameObject); // Optionally destroy the item in the scene after collection
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) { // Ensure the player is the one collecting
            Collect();
        }
    }
}
