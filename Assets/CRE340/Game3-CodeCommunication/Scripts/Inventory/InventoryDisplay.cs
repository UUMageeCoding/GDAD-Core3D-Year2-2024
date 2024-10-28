using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour {
    
    public GameObject slotPrefab; // Prefab for individual inventory slots
    public Transform slotParent;  // Parent object for slots in the UI panel
    private List<GameObject> slots = new List<GameObject>();

    private void OnEnable() {
        InventoryManager.OnInventoryChanged += UpdateInventoryUI;
    }

    private void OnDisable() {
        InventoryManager.OnInventoryChanged -= UpdateInventoryUI;
    }

    private void UpdateInventoryUI(List<InventoryItem> items) {
        // Clear previous slots
        foreach (GameObject slot in slots) {
            Destroy(slot);
        }
        slots.Clear();

        // Create a new slot for each item
        foreach (InventoryItem item in items) {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            slots.Add(newSlot);

            // Update the slot with item data
            Image icon = newSlot.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI nameText = newSlot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI quantityText = newSlot.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

            icon.sprite = item.icon;
            nameText.text = item.itemName;
            quantityText.text = items.FindAll(i => i == item).Count.ToString(); // Show stack count if needed
        }
    }
}