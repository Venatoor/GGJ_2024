using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private PlayerLocomotion playerLocomotion;
    private InputManager inputManager;
    private CameraManager cameraManager;
    private LaughStat laughStat;
    private Animator animator;
    private GameManager gameManager;

    public bool isInteracting;


    private void InitializeComponents()
    {
        gameManager = gameObject.AddComponent<GameManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        if ( playerLocomotion == null )
        {
            playerLocomotion = gameObject.AddComponent<PlayerLocomotion>();
        }

        inputManager = GetComponent<InputManager>();
        if ( inputManager == null )
        {
            inputManager = gameObject.AddComponent<InputManager>();
        }

        laughStat = GetComponent<LaughStat>();
        if ( laughStat == null )
        {
            laughStat = gameObject.AddComponent<LaughStat>();
        }
        animator = GetComponent<Animator>();
        laughStat.SetMaxAmount(100f);
        laughStat.SetAmount(50f);
    }

    private void Awake()
    {
        InitializeComponents();
    }

    // Update is called once per frame
    private void Update()
    {
        inputManager.HandleAllInputs(gameManager);
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovements();
    }

    private void LateUpdate()
    {
        if (!gameManager.GetPaused()) {
            cameraManager.HandleAllCameraMovement();
        }

        isInteracting = animator.GetBool("isInteracting");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }


   
}
