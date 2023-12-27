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
    private int[] inventory;

    void Start()
    {
        // Reset Inventory to 0
        inventory = new int[3];
        for (int i = 0; i < 3; i++)
        {
            inventory[i] = 0;
        }
        LogInventory();
    }

    void LogInventory() {
        string inventoryStr = "Inventory:\nArrows: {0}\nExplosive Arrows: {1}\nSecrets: {2}";
        inventoryText.text = string.Format(inventoryStr, inventory[0], inventory[1], inventory[2]);
    }

    public bool HasRangeAttackAmmo()
    {
        return inventory[0] > 0 || inventory[1] > 0;
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
