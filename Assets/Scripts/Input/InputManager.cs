using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    CharacterControl playerControls;
    AnimationManager animatorManager;
    [SerializeField]
    private Vector2 movementInput;
    private Vector2 cameraInput;

    public float verticalInput;
    public float horizontalInput;

    private float moveAmount;

    public float cameraInputX;
    public float cameraInputY;

    private void Awake()
    {
        animatorManager = GetComponent<AnimationManager>();

    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new CharacterControl();

            // Set up movement and camera input
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            // Set up mouse click input
            playerControls.PlayerMovement.Mouse.started += _ => OnMouseClick();

            // Enable the controls
            playerControls.Enable();
        }
    }

    private void OnDisable()
    {
        // Disable the controls
        playerControls.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimator(0, moveAmount);
        

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
    }

    // New method to handle mouse click
    private void OnMouseClick()
    {
        isMouseClick = true;
        // Handle additional logic for mouse click if needed
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();

        // Check if there was a mouse click
        if (isMouseClick)
        {
            // Handle mouse click logic
            Debug.Log("Mouse Clicked!");
            isMouseClick = false; // Reset the flag
        }

        // Add logic for other inputs (e.g., jump, interact, dash) here
    }
}
