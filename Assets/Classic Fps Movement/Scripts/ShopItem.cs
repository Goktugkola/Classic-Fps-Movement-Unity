using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    public Inventory ShopInventory;
    public Inventory playerInventory;
    private GameObject player;
    
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    public void RemoveItem(GameObject itemObject)
    {
        InventoryItem item = ShopInventory.items.Find(item => item.itemName == itemObject.name);
        
        if (ShopInventory.items.Contains(item))
        {
            playerInventory.items.Add(item);
            ShopInventory.items.Remove(item);
            Destroy(gameObject);
            // Update UI or perform other actions for removing an item
        }
    }
}
