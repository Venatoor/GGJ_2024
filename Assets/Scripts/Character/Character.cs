using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private PlayerLocomotion playerLocomotion;
    private InputManager inputManager;
    private CameraManager cameraManager;
    private LaughStat laughStat;
    private void InitializeComponents()
    {
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
    }

    private void Awake()
    {
        InitializeComponents();
    }

    // Update is called once per frame
    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovements();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
