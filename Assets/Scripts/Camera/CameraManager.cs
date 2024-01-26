using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Transform targetTransform;
    public Transform cameraPivot;
    private InputManager inputManager;

    private Vector3 cameraFollowVelocity = Vector3.zero;

    public float cameraFollowSpeed = 0.01f;
    public float cameraLookSpeed = 0.001f;
    public float cameraPivotSpeed = 0.001f;

    public float pivotMinimumAngle = -35f;
    public float pivotMaximumAngle = 35f;

    public float lookAngle; // camera looking up and down
    public float pivotAngle; // camera looking right and left
    // Start is called before the first frame update

    private void Awake()
    {
        targetTransform = FindObjectOfType<Character>().transform;
        inputManager = FindObjectOfType<InputManager>();

    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;

    }

    public void RotateCamera()
    {
        lookAngle = lookAngle + ( inputManager.cameraInputX * cameraLookSpeed );
        pivotAngle = pivotAngle - ( inputManager.cameraInputY * cameraPivotSpeed );
        pivotAngle = Mathf.Clamp(pivotAngle, pivotMinimumAngle, pivotMaximumAngle);
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
}
