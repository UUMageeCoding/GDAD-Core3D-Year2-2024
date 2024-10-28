using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Health Item
[CreateAssetMenu(fileName = "NewHealthItem", menuName = "Inventory/Health Item")]
public class HealthItem : InventoryItem {
    public int healingAmount;

    private void OnEnable() {
        itemType = ItemType.Health;
    }
}

