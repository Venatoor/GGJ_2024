using System.Collections;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    private float launchAngle = 75f;
    public float ejectionForceFactor ;
    public Transform directionObject;
    private Character character;

    private float x0;
    private float y0;
    private float z0;

    private void Start()
    {
        character = FindObjectOfType<Character>();
        x0 = transform.position.x;
        y0 = transform.position.y;
        z0 = transform.position.z;
    }

    public void Execute()
    {
        float v0, time;
        CalculatePath(launchAngle * Mathf.Deg2Rad, out v0, out time);

        // Increase the ejection force
        v0 *= ejectionForceFactor;

        StopAllCoroutines();
        StartCoroutine(ParabolaMovement(v0, launchAngle * Mathf.Deg2Rad, time));
    }

    private void CalculatePath(float angle, out float v0, out float time)
    {
        float g = -Physics.gravity.y;

        // Update x0 based on the character's position
        x0 = character.transform.position.x;

        // Calculate the launch velocity and time of flight
        v0 = Mathf.Sqrt(Mathf.Abs(g) * x0 / Mathf.Sin(2 * angle));
        time = (2 * v0 * Mathf.Sin(angle)) / Mathf.Abs(g);
    }

    private IEnumerator ParabolaMovement(float v0, float angle, float time)
    {
        float t = 0;
        while (t < time)
        {
            // Use directionObject's X-axis as the trajectory's X-axis
            Vector3 trajectoryDirection = directionObject.transform.forward;

            float z = v0 * t * Mathf.Cos(angle) + z0; 
            float x = v0 * t * Mathf.Cos(angle) + x0;
            float y = v0 * Mathf.Sin(angle) * t - (0.5f) * Mathf.Abs(Physics.gravity.y) * Mathf.Pow(t, 2) + y0;


            Vector3 newPosition = new Vector3(x, y, z);

            // Rotate the newPosition based on the trajectoryDirection
            newPosition = Quaternion.LookRotation(trajectoryDirection, Vector3.up) * newPosition;

            character.transform.position = newPosition;

            t += Time.deltaTime;
            yield return null;
        }
    }
}
