using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    CharacterControl playerControls;
    AnimationManager animatorManager;
    PlayerLocomotion playerLocomotion;

    [SerializeField]
    private Vector2 movementInput;
    private Vector2 cameraInput;

    public float verticalInput;
    public float horizontalInput;

    private float moveAmount;

    public float cameraInputX;
    public float cameraInputY;

    public bool jumpInput;
    public bool pauseInput;
    private void Awake()
    {
        animatorManager = GetComponent<AnimationManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        

    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new CharacterControl();

            // Set up movement and camera input
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
            playerControls.PlayerActions.Pause.performed += i => pauseInput = true;

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



    public void HandleAllInputs(GameManager gm)
    {
        HandleMovementInput();
        HandleJumpInput();
        HandlePauseInput(gm);

        // Add logic for other inputs (e.g., jump, interact, dash) here
    }

    private void HandleJumpInput()
    {
        if ( jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }
    private void HandlePauseInput(GameManager gm) {
        if (pauseInput) {
            pauseInput = false;
            gm.PauseGame();
        }
    }
}
