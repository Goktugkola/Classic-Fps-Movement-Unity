using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public ViewManager ShopMenu;
    public ViewManager InventoryMenu;
    public PlayerInput playerInput;
    private bool shopopen = false;
    private bool invopen = false;
    void Update()
    {
        if (playerInput.actions["Shop"].triggered)
        {
            if (!shopopen)
            {
                playerInput.actions["Pause"].Enable();
                ShopMenu.PlayScene();
                shopopen = true;
            }
            else
            {
                shopopen = false;
                ShopMenu.UnloadScene();
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerInput.actions["Pause"].Disable();
            }

        }
        if (playerInput.actions["Inventory"].triggered)
        {
            if (!invopen)
            {
                playerInput.actions["Pause"].Enable();
                InventoryMenu.PlayScene();
                invopen = true;
            }
            else
            {
                invopen = false;
                InventoryMenu.UnloadScene();
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerInput.actions["Pause"].Disable();
            }

        }
    }
}
