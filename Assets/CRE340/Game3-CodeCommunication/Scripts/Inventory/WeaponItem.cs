using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Weapon Item
[CreateAssetMenu(fileName = "NewWeaponItem", menuName = "Inventory/Weapon Item")]
public class WeaponItem : InventoryItem {
    public int damage;
    public int ammoCount;

    private void OnEnable() {
        itemType = ItemType.Weapon;
    }
}
