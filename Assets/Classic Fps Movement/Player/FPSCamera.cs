using UnityEngine.InputSystem;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] private PlayerInput playerInput;
    public float sensitivity = 50f;
    public Transform player;

    private float xRotation = 0f;
    [SerializeField] private float interactionDistance = 10f;
    public Inventory inventory;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Look();
        if(playerInput.actions["Interact"].triggered)
        {
            Interact();
        }

    }
    
    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                Debug.Log("Interacted with object");
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("item"))
                {
                    InventoryItem newItem = new InventoryItem();
                    newItem.itemName = hit.collider.gameObject.name;
                    newItem.itemID = hit.collider.gameObject.GetComponent<ItemData>().itemID;
                    newItem.itemIcon = hit.collider.gameObject.GetComponent<ItemData>().itemIcon;
                    Debug.Log("Item picked up");
                    // Add item to inventory
                    inventory.AddItem(newItem);
                    Destroy(hit.collider.gameObject, 1f);
                }
            }
        }
    }
    void Look()
    {
        float mouseX = playerInput.actions["Look"].ReadValue<Vector2>().x * sensitivity * Time.deltaTime;
        float mouseY = playerInput.actions["Look"].ReadValue<Vector2>().y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
