using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour 
{
    public InventoryItem itemData; // Reference to the ScriptableObject

    private InventoryManager inventoryManager; // Reference to the InventoryManager - we will use this to add the item to the inventory


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) { // Ensure the player is the one collecting
            GameObject player = other.gameObject;
            if (player.GetComponent<InventoryManager>() != null){
                inventoryManager = player.GetComponent<InventoryManager>(); // Get the InventoryManager reference
                Collect(); // Collect the item
            }
        }
    }
    
    public void Collect() {
        // Get the InventoryManager reference
        inventoryManager.AddItem(itemData); // Add the item to the inventory
        PostCollect();
    }

    private void PostCollect(){
        Destroy(gameObject); // Optionally destroy the item in the scene after collection
    }
}