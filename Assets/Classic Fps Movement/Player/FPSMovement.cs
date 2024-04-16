using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] private PlayerInput playerInput;
    [Header("Movement")]
    public float walkSpeed = 150f;
    public float sprintSpeed = 400f;
    public float acceleration = 10f;
    public float airAcceleration = 2f;
    public float jumpForce = 8f;
    public float gravity = 20f;
    public float maxSpeed = 500f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Head Bob")]
    public bool useHeadBob = true;
    public float stepInterval = 5f;

    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity = 1f;

    private CharacterController controller;
    public Vector3 moveDirection;
    public Vector3 velocity;
    private bool isGrounded;
    private bool canBunny;
    private float speed;
    private bool isSprinting;
    private bool wishJump;
    private float nextStep;
    private float bobTimer;
    private Vector3 originalCameraPosition;
    private float currentHorizontalSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalCameraPosition = Camera.main.transform.localPosition;
    }

    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        canBunny = Physics.CheckSphere(groundCheck.position, groundDistance + 0.5f, groundMask);

        // Sprint
        isSprinting = playerInput.actions["Sprint"].IsPressed();

        // Jump
        if (playerInput.actions["Jump"].triggered && isGrounded || playerInput.actions["Jump"].triggered && canBunny)
        {
            wishJump = true;
        }

        // Head Bobbing
        if (useHeadBob && isGrounded && controller.velocity.magnitude > 0f)
        {
            bobTimer += Time.deltaTime * (isSprinting ? sprintSpeed : walkSpeed) / stepInterval;
            float bobbingAmount = Mathf.Sin(bobTimer) * 0.05f;
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x,
                                                             originalCameraPosition.y + bobbingAmount,
                                                             Camera.main.transform.localPosition.z);
        }
    }

    void FixedUpdate()
    {
        // Input
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;
        speed = isSprinting ? sprintSpeed : walkSpeed;

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
        Vector3 wishDir = moveDirection; // Preserve existing velocity
        if (isGrounded)
        {
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, wishDir.magnitude * speed, acceleration * Time.deltaTime);
            currentHorizontalSpeed = Mathf.Min(currentHorizontalSpeed, maxSpeed);
            if (wishJump)
            {
                currentHorizontalSpeed -= 1;
                velocity.y = jumpForce;
            }
            wishJump = false; // Move this line outside of the if statement
            velocity.y = -gravity < velocity.y ? velocity.y : velocity.y - gravity * Time.deltaTime;
            
        }
        else
        {

            velocity.y -= gravity * Time.deltaTime;
        }
        if (canBunny)
        {
            if (wishJump)
            {
                velocity.y = jumpForce;
            }
            wishJump = false; // Move this line outside of the if statement
        }
        // Air Strafing
        if (!isGrounded)
        {
            float mouseX = playerInput.actions["Look"].ReadValue<Vector2>().x * mouseSensitivity / 10f;


            if (moveDirection.x != 0f) { wishDir += transform.forward * mouseX * airAcceleration / 10f; } // Add mouse movement influence    
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, wishDir.magnitude * speed, airAcceleration * Time.deltaTime);
            currentHorizontalSpeed = Mathf.Min(currentHorizontalSpeed, maxSpeed);
        }

        // Move the player
        moveDirection.x = wishDir.x * currentHorizontalSpeed;
        moveDirection.z = wishDir.z * currentHorizontalSpeed;
        controller.Move(moveDirection * speed * Time.deltaTime + velocity * Time.deltaTime);

    }


}