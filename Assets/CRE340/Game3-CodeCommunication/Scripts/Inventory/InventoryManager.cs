using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static event Action<List<InventoryItem>> OnInventoryChanged;
    public List<InventoryItem> items = new List<InventoryItem>();
    private const int MaxItems = 8; // Limit inventory to 8 items

    public bool CanAddItem() {
        return items.Count < MaxItems; // Returns true if there's room for more items
    }

    public void AddItem(InventoryItem newItem) {
        if (items.Count < MaxItems) {
            items.Add(newItem);
            OnInventoryChanged?.Invoke(items); // Notify UI about inventory change
        } else {
            Debug.Log("Inventory is full");
        }
    }

    public void RemoveItem(InventoryItem item) {
        if (items.Contains(item)) {
            items.Remove(item);
            OnInventoryChanged?.Invoke(items); // Notify UI about inventory change
        }
    }
    
    // remove an item by calling the RemoveItem method when  R is pressed IN UPDATE - for testing purposes
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            if (items.Count > 0) {
                RemoveItem(items[0]);
            }
        }
    }
}