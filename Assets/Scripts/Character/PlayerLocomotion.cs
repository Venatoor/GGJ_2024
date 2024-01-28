using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    AnimationManager animationManager;
    Animator animator;

    public float leapingVelocity;
    public float fallingVelocity;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;

    float gravity = -15f;
    float height = 3f;

    Vector3 jumpDirection;

    public float inAirTimer;

    public bool isJumping;

    [SerializeField]
    private float movementSpeed = 50f;
    [SerializeField]
    private float rotationSpeed = 50f;


    public LayerMask groundLayer;

    private float speedFactor = 1f;

    Character player;

    public bool isGrounded;

    [SerializeField]
    private float rayCastHeightOffset;

    // Start is called before the first frame update
    private void Awake()
    {
        isGrounded = true;
        rayCastHeightOffset = 1.1f;
        player = FindObjectOfType<Character>();
        animationManager = GetComponent<AnimationManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidBody = player.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraObject = Camera.main.transform;

        if ( speedFactor > 2 )
        {
            Mathf.Clamp(speedFactor, 1, 2);
        }
    }


    public void HandleMovement()
    {
        if ( isJumping)
        {
            return;
        }

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed *  Time.deltaTime * speedFactor;

        Vector3 movementVelocity = moveDirection;
        playerRigidBody.velocity = movementVelocity;
    }

    public void HandleRotation()
    {
        if ( isJumping)
        {
            return;
        }

        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();

        if ( targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        playerRotation.x = 0;
        playerRotation.z = 0;

        transform.rotation = playerRotation;

    }


    public void HandleAllMovements()
    {
        HandleFallingAndLanding();
        if (player.isInteracting)
        {
            return;
        }
        HandleMovement();
        HandleRotation();
    }


    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if ( !isGrounded && !isJumping )
        {
            if ( !player.isInteracting)
            {
                animationManager.PlayTargetAnimation("Fall", true);

            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }


        if ( Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if ( !isGrounded && !player.isInteracting )
            {
                animationManager.PlayTargetAnimation("Land", true);
            }
            inAirTimer = 0;
            isGrounded = true;
            player.isInteracting = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    public float GetSpeedFactor()
    {
        return speedFactor;
    }

    public void SetSpeedFactor(float amount)
    {
        speedFactor = amount;
    }

    public void HandleJumping()
    {
        if ( isGrounded )
        {
            animationManager.animator.SetBool("isJumping", true);
            animationManager.PlayTargetAnimation("Jump", false);
            float jumpingVelocity = Mathf.Sqrt(-2 * gravity * height);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;

        }
    }

    public void HandleEject()
    {

        animationManager.animator.SetBool("isJumping", true);
        animationManager.PlayTargetAnimation("Jump", false);
        float jumpingVelocity = 3 * Mathf.Sqrt(-2 * gravity * height);
        Vector3 playerVelocity = moveDirection;
        playerVelocity.y = jumpingVelocity;
        playerRigidBody.velocity = playerVelocity;

    
    }

}
