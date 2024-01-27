using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neelde : MonoBehaviour
{
    public RectTransform uiElement;
    public Character character;
    public float rotationSpeed = 5f;
    public float minRotation = -50f;
    public float maxRotation = 50f;

    private float rotationValue;
    private LaughStat laughStat;

    void Start() {
        laughStat = character.GetComponent<LaughStat>();
    }
    void Update()
    {

        rotationValue = -1 * (laughStat.GetAmount() - 50);
        // Assuming you want to rotate around the Z-axis
        rotationValue = Mathf.Clamp(rotationValue, minRotation, maxRotation);

        // Calculate the new rotation angle based on the speed
        float currentRotationAngle = uiElement.localRotation.eulerAngles.z;
        float newRotationAngle = Mathf.LerpAngle(currentRotationAngle, rotationValue, Time.deltaTime * rotationSpeed);

        // Apply the new rotation to the RectTransform
        uiElement.localRotation = Quaternion.Euler(0f, 0f, newRotationAngle);
    }
}
