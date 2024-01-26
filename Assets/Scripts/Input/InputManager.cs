using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    
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
        if ( playerControls == null)
        {
            playerControls = new CharacterControl();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
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

    public void HandleAllInputs()
    {
        HandleMovementInput();
        //HandleJumpInput;
        //HandleInteractInput;
        //HandleDashInput;
    }
}
