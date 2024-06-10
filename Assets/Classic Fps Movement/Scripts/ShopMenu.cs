using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private Transform panel;
    public PlayerInput playerInput;
    public static bool GameIsPaused = true;
    public Inventory inventory;
    public GameObject itemPrefab; // Prefab for individual item UI

    void Start()
    {
        PopulateInventory();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        Pause();

    }
 


    public void Pause()
    {
        PopulateInventory();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        panel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        playerInput.actions["Pause"].Disable();
    }
    public void PopulateInventory()
    {
        foreach (Transform child in panel) { if (child.CompareTag("Item")) Destroy(child.gameObject); }
        // Add items to the inventory
        foreach (InventoryItem inventoryItem in inventory.items)
        {
            GameObject itemObject = Instantiate(itemPrefab, panel);
            // Set item properties in the UI element
            itemObject.name = inventoryItem.itemName;
            itemObject.GetComponentInChildren<TextMeshProUGUI>().text = inventoryItem.itemName;
            itemObject.GetComponentInChildren<Image>().sprite = inventoryItem.itemIcon;
            // Add other properties and functionality as needed
        }
    }
}
