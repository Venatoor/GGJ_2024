using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationType
{
    RotationX,
    RotationY,
    RotationZ
}


public class RotatingPlatform : Platform
{


    public float lerpDuration;
    public bool isRotating;
    public int rotationAngle;
    public float delayTime;
    public float activationTime;

    [SerializeField]
    private RotationType rotationType;

    public Collider col;
    private void Start()
    {
        col = GetComponent<Collider>();
        isRotating = false;
        delayTime = lerpDuration + 0.2f;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (!isRotating)
        {
            StartCoroutine(OnTouch());
        }
    }

    public override void OnCollisionExit(Collision collision)
    {
    }

    public IEnumerator OnTouch()
    {
        isRotating = true;
        yield return new WaitForSeconds(activationTime);
        col.enabled = false;
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation;
        switch ( rotationType )
        {
            case RotationType.RotationX:
                targetRotation = transform.rotation * Quaternion.Euler(rotationAngle, 0, 0);
                break;
            case RotationType.RotationY:
                targetRotation = transform.rotation * Quaternion.Euler(0, rotationAngle, 0);
                break;
            case RotationType.RotationZ:
                targetRotation = transform.rotation * Quaternion.Euler(0, 0, rotationAngle);
                break;
            default:
                targetRotation = transform.rotation * Quaternion.Euler(0, rotationAngle, 0);
                break;
        }
        while (timeElapsed < lerpDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        col.enabled = true;
        yield return new WaitForSeconds(delayTime);
        isRotating = false;
    }


}
