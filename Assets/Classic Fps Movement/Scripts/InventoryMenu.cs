using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private Transform panel;
    public PlayerInput playerInput;
    public static bool GameIsPaused = false;
    public Inventory inventory;
    public GameObject itemPrefab; // Prefab for individual item UI
    public Transform contentPanel; // Panel to hold item UI elements

    void Start()
    {
        PopulateInventory();
        contentPanel.gameObject.SetActive(false);
    }
    void Update()
    {
        if (playerInput.actions["Inventory"].triggered)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();

            }
        }
    }

    public void Resume()
    {
        PopulateInventory();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        contentPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        playerInput.actions["Pause"].Enable();

    }
    public void Pause()
    {
        PopulateInventory();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        contentPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        playerInput.actions["Pause"].Disable();
    }
    public void PopulateInventory()
    {
        foreach(Transform child in panel){if(child.CompareTag("Item")) Destroy(child.gameObject);}
        // Add items to the inventory
        foreach (InventoryItem inventoryItem in inventory.items)
        {
            GameObject itemObject = Instantiate(itemPrefab, contentPanel);
            // Set item properties in the UI element
            itemObject.name = inventoryItem.itemName;
            itemObject.GetComponentInChildren<TextMeshProUGUI>().text = inventoryItem.itemName;
            itemObject.GetComponentInChildren<Image>().sprite = inventoryItem.itemIcon;
            // Add other properties and functionality as needed
        }
    }
}
