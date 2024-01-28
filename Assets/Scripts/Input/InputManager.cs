using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    CharacterControl playerControls;
    Character character;
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
    public bool ejectInput;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
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
            playerControls.PlayerActions.Eject.performed += i => ejectInput = true;


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
        // Update camera input regardless of whether the character is in the canon
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        if (!character.GetIsInCanon())
        {
            // Update character movement only if not in the canon
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateAnimator(0, moveAmount);
        }
        else
        {
            // Character is in the canon, set movement inputs to zero
            verticalInput = 0f;
            horizontalInput = 0f;
            moveAmount = 0f;
            animatorManager.UpdateAnimator(0, 0);
        }
    }

    public void HandleAllInputs(GameManager gm)
    {
            HandleEjectInput();

            //HandlePauseInput(gm);

            HandleMovementInput();
            HandleJumpInput();
        
        // Add logic for other inputs (e.g., jump, interact, dash) here
    }

    private void HandleJumpInput()
    {
        if (!character.GetIsInCanon())
        {
            if (jumpInput)
            {
                jumpInput = false;
                playerLocomotion.HandleJumping();
            }
        }
    }

    private void HandleEjectInput()
    {

        if (ejectInput && character.GetIsInCanon())
        {
            // Set the character as not in the canon
            character.SetInCanonFalse();
            // Trigger the parabolic movement coroutine
            ejectInput = false;
            
            playerLocomotion.HandleEject();
            ScaleUpObject();
        }
    }

    public void HandlePauseInput(GameManager gm)
    {
        if (pauseInput)
        {
            pauseInput = false;
            gm.SwitchGameManageState();
        }
    }


    private void ScaleUpObject()
    {
        Vector3 newScale = character.transform.localScale * (1f / character.scaleDownFactor);
        character.transform.localScale = newScale;
    }
}
