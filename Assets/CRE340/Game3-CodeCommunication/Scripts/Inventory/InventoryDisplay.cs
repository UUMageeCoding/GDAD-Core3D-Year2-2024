using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour {
    public GameObject slotPrefab;     // Slot prefab to represent each inventory item
    public Transform slotParent;      // Parent transform for positioning slots
    public float xSpacing = 128f;     // Horizontal spacing between slots
    public float ySpacing = 128f;     // Vertical spacing between slots

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

        // Create a new slot for each item in the inventory, arranged in a 2x4 grid
        for (int i = 0; i < items.Count; i++) {
            InventoryItem item = items[i];
            GameObject newSlot = Instantiate(slotPrefab, slotParent);

            // Calculate grid position
            int row = i / 2;     // 2 items per row
            int column = i % 2;  // 0 for first item in row, 1 for second item

            // Position slot based on calculated grid position
            newSlot.transform.localPosition = new Vector3(column * xSpacing, -row * ySpacing, 0);
            slots.Add(newSlot);

            // Update the slot with item data
            Image icon = newSlot.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI nameText = newSlot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

            icon.sprite = item.icon;
            nameText.text = item.itemName;
        }
    }
}