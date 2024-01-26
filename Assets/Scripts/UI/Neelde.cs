using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neelde : MonoBehaviour
{
    public RectTransform uiElement;
    public float rotationValue;
    public float rotationSpeed = 5f;
    public float minRotation = -55f;
    public float maxRotation = 50f;
    private float timer = 1f; 


    void Update()
    {

        // Update the timer
        timer -= Time.deltaTime;

        // Decrease the rotation value every second
        if (timer <= 0f)
        {
            rotationValue = Mathf.Max(minRotation, rotationValue + 3); // Adjust the decrement value
            timer = 1f; // Reset the timer
        }

        // Assuming you want to rotate around the Z-axis
        rotationValue = Mathf.Clamp(rotationValue, minRotation, maxRotation);

        // Calculate the new rotation angle based on the speed
        float currentRotationAngle = uiElement.localRotation.eulerAngles.z;
        float newRotationAngle = Mathf.LerpAngle(currentRotationAngle, rotationValue, Time.deltaTime * rotationSpeed);

        // Apply the new rotation to the RectTransform
        uiElement.localRotation = Quaternion.Euler(0f, 0f, newRotationAngle);
    }
}
