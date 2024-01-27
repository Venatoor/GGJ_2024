using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour, IMovement
{

    [SerializeField]
    float _angle;
    [SerializeField]
    Transform tempPosition;
    [SerializeField]
    float height;
    Vector3 originalPosition;

    public bool isMovementExecuted;

    float x0;
    float y0;

    private void Start()
    {
        originalPosition = transform.position;
        x0 = transform.position.x;
        isMovementExecuted = false;
        y0 = transform.position.y;
    }
    public void Execute()
    {
        //float angle = _angle * Mathf.Deg2Rad;
        Vector3 targetPos = tempPosition.position;
        float v0;
        float time;
        float angleNoHeight = _angle * Mathf.Deg2Rad;
        float angle;
        CalculatePath(targetPos, angleNoHeight, out v0, out time);
        //CalculatePathWithHeight(targetPos, height, out v0, out time, out angle);
        if (!isMovementExecuted)
        {
            x0 = transform.position.x;
            y0 = transform.position.y;
        }

        if (Input.GetKeyDown(KeyCode.U) && !isMovementExecuted)
        {
            isMovementExecuted = true;
            StopAllCoroutines();
            StartCoroutine(ParabolaMovement(v0, angleNoHeight, time));
        }
    }

    private void Update()
    {
        Execute();
    }

    private float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    private void CalculatePath(Vector3 targetPos, float angle, out float v0, out float time)
    {

        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float v1 = Mathf.Pow(xt - x0, 2) * g;
        float v2 = 2 * (xt - x0) * Mathf.Sin(angle) * Mathf.Cos(angle);
        float v3 = 2 * (yt - y0) * Mathf.Pow(Mathf.Cos(angle), 2);

        v0 = Mathf.Sqrt(v1 / (v2 - v3));

        time = (xt - x0) / (v0 * Mathf.Cos(angle));
    }

    private void CalculatePathWithHeight(Vector3 targetPos, float h, out float v0, out float time, out float angle)
    {

        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float a = -0.5f * g;
        float b = Mathf.Sqrt(2 * g * (h));
        float c = -yt;

        float tplus = QuadraticEquation(a, b, c, +1);
        float tmin = QuadraticEquation(a, b, c, -1);
        time = tplus > tmin ? tplus : tmin;

        angle = Mathf.Atan(b * time / xt);
        v0 = b / Mathf.Sin(angle);
    }

    private IEnumerator ParabolaMovement(float v0, float angle, float time)
    {

        float t = 0;
        while (t < 10)
        {


            float x = v0 * t * Mathf.Cos(angle) + x0;
            float y = v0 * Mathf.Sin(angle) * t - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2) + y0;

            transform.position = new Vector3(x, y, 0);
            t += Time.deltaTime;

            yield return null;
        }
        isMovementExecuted = false;
    }



    //TODO ADD XO AND YO IN ALL EQUATIONS
}
