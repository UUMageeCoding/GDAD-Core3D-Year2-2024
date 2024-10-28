using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static event Action<List<InventoryItem>> OnInventoryChanged;

    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(InventoryItem item) {
        if (items.Count < 10) { // Limit inventory to 10 items
            items.Add(item);
            OnInventoryChanged?.Invoke(items); // Send updated inventory list
        }
    }

    public void RemoveItem(InventoryItem item) {
        items.Remove(item);
        OnInventoryChanged?.Invoke(items); // Send updated inventory list
    }
}