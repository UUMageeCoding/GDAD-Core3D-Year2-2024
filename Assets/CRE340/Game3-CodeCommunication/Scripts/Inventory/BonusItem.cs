using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bonus Item
[CreateAssetMenu(fileName = "NewBonusItem", menuName = "Inventory/Bonus Item")]
public class BonusItem : InventoryItem 
{
    public int bonusPoints;
    private void OnEnable() {
        itemType = ItemType.Bonus;
    }
}
