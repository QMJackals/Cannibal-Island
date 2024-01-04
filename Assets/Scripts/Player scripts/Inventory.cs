using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum InventoryItemType
{
    ARROW,
    EXPLOSIVE_ARROW,
    SECRET,
};

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;
    public TextMeshProUGUI currentSelectionText;

    private InventoryItemType currentSelection;
    private int[] inventory;

    void Start()
    {
        // Reset Inventory to 0
        inventory = new int[3];
        for (int i = 0; i < 3; i++)
        {
            inventory[i] = 0;
        }
        // Set current selection to arrow
        currentSelection = InventoryItemType.ARROW;
        // Log current inventory
        LogInventory();
        // Log current selection
        LogCurrentSelection();
    }

    void LogInventory() {
        string inventoryStr = "Inventory:\nArrows: {0}\nExplosive Arrows: {1}\nSecrets: {2}/10";
        inventoryText.text = string.Format(inventoryStr, inventory[0], inventory[1], inventory[2]);
    }

    void LogCurrentSelection()
    {
        string currentSelectionStr = currentSelection == InventoryItemType.ARROW ? "Arrows" : "Explosive Arrows";
        currentSelectionText.text = $"Selected: {currentSelectionStr}";
    }

    // Returns if there is any currently selected range attack ammo in the inventory
    public bool HasRangeAttackAmmo()
    {
        return inventory[(int)currentSelection] > 0;
    }

    // Get the current selection
    public InventoryItemType GetCurrentSelection()
    {
        return currentSelection;
    }

    // Switch currently selected between Arrow and Explosive Arrow
    public void ToggleCurrentSelection()
    {
        if (currentSelection == InventoryItemType.ARROW)
        {
            currentSelection = InventoryItemType.EXPLOSIVE_ARROW;
        }
        else if (currentSelection == InventoryItemType.EXPLOSIVE_ARROW)
        {
            currentSelection = InventoryItemType.ARROW;
        }
        LogCurrentSelection();
    }

    // Add items to inventory
    public void AddItem(InventoryItemType itemType, int qty)
    {
        if (qty < 0)
        {
            qty = -qty;
        }
        UpdateInventoryItems((int)itemType, qty);
    }

    // Remove items from inventory
    public void RemoveItem(InventoryItemType itemType, int qty)
    {
        if (qty > 0)
        {
            qty = -qty;
        }
        UpdateInventoryItems((int)itemType, qty);
    }

    // Helper method to update inventory count
    private void UpdateInventoryItems(int itemIdx, int qty)
    {
        inventory[itemIdx] += qty;
        LogInventory();
    }
}
