using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float amplitude = 45f;   // Max angle of swing in degrees
    public float frequency = 1f;    // Oscillation frequency in Hz

    private float angleOffset;      // Angle offset for starting position

    void Start()
    {
        angleOffset = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // Calculate the angle based on time and frequency
        float angle = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * Time.time);

        // Apply the angle to the rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + angleOffset);
        transform.rotation = targetRotation;
    }
}
