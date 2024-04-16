using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Inventory inventory;
    public void RemoveItem(GameObject itemObject)
    {
        InventoryItem item = inventory.items.Find(item => item.itemName == itemObject.name);
        if (inventory.items.Contains(item))
        {
            
            inventory.items.Remove(item);
            Destroy(gameObject);
            // Update UI or perform other actions for removing an item
        }
    }
}
