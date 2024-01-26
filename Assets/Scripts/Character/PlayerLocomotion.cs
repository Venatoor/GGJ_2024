using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;

    [SerializeField]
    private float movementSpeed = 50f;
    [SerializeField]
    private float rotationSpeed = 50f;

    private float speedFactor = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        Character player = FindObjectOfType<Character>();
        inputManager = GetComponent<InputManager>();
        playerRigidBody = player.GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

        if ( speedFactor > 2 )
        {
            Mathf.Clamp(speedFactor, 1, 2);
        }
    }

    public void HandleMovement()
    {
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

        transform.rotation = playerRotation;

    }

    public void HandleJump()
    {

    }

    public void FreezeRotations()
    {
        playerRigidBody.freezeRotation = true;
    }

    public void HandleAllMovements()
    {
        HandleMovement();
        HandleRotation();
    }
}
