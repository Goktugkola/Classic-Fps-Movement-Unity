using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory")]

public class Inventory : ScriptableObject
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public int inventorySize = 20; // Maximum number of items


    public void AddItem(InventoryItem item)
    {
        if (items.Count < inventorySize)
        {
            items.Add(item);
            
            // Update UI or perform other actions for adding an item
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void RemoveItem(InventoryItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            // Update UI or perform other actions for removing an item
        }
    }
}