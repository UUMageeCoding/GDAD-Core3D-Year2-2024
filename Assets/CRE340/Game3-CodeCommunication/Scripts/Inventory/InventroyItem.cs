using UnityEngine;

public abstract class InventoryItem : ScriptableObject {
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
}