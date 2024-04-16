using UnityEngine;
using TMPro;

public class SpeedDisplay : MonoBehaviour
{
    private GameObject player;
    public PlayerMovement playerMovement; // Reference to your player movement script
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Calculate and display speed
        Vector3 horizontalVelocity = new Vector3(playerMovement.moveDirection.x, 0f, playerMovement.moveDirection.z);
        float speed = horizontalVelocity.magnitude;
        textMeshPro.text = "Speed: " + speed.ToString("F1") + " u/s"; // Format the speed value (e.g., to one decimal place)
    }
}