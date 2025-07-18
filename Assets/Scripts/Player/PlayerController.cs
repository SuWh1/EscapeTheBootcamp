using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float verticalLookLimit = 90f;
    
    [Header("Ground Detection")]
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayerMask = 1;
    
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    
    // Private variables
    private Rigidbody playerRigidbody;
    private float verticalRotation = 0f;
    private Vector3 moveDirection;
    private bool isGrounded;
    
    void Start()
    {
        // Get components
        playerRigidbody = GetComponent<Rigidbody>();
        
        // If camera not assigned, try to find it as a child
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }
        
        // Lock cursor for FPS-style control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Freeze rotation on rigidbody to prevent physics from affecting rotation
        playerRigidbody.freezeRotation = true;
    }
    
    void Update()
    {
        CheckGrounded();
        HandleInput();
        HandleMouseLook();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void HandleInput()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D keys
        float vertical = Input.GetAxis("Vertical");     // W/S keys
        
        // Calculate movement direction relative to player's rotation
        moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection = moveDirection.normalized;
        
        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }
    
    private void HandleMovement()
    {
        // Apply movement using Rigidbody for proper physics collision
        Vector3 targetPosition = playerRigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(targetPosition);
    }
    
    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Rotate player horizontally (Y-axis)
        transform.Rotate(Vector3.up * mouseX);
        
        // Rotate camera vertically (X-axis) with clamping
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
        
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
    
    private void CheckGrounded()
    {
        // Cast a ray downward to check if player is on ground
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, groundCheckDistance + 0.1f, groundLayerMask);
    }
    
    private void Jump()
    {
        // Apply upward force for jump
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    void OnApplicationFocus(bool hasFocus)
    {
        // Re-lock cursor when game regains focus
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
} 