using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Platform
{
    [SerializeField]
    private Transform PositionA;

    [SerializeField]
    private Transform PositionB;

    private Vector3 currentPosition;

    private Vector3 targetedPosition;

    private Transform temp;

    private Collider col;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float delayTime;

    private bool isMoving = false;

    private Rigidbody rb;

    private float distance;

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (!isMoving)
        {
            isMoving = true;
            Invoke("ActivateElevator", CalculateTime(speed, distance) + delayTime);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }

    private void Start()
    {
        temp = null;
        isMoving = false;
        currentPosition = PositionA.position;
        targetedPosition = PositionB.position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        distance = Vector2.Distance(currentPosition, targetedPosition);
    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isMoving)
            OnTouch(transform.position, targetedPosition);
    }
    //TO ADD TO AN ALGORITHM CLASS
    private void Permute()
    {
        temp = PositionA;
        PositionA = PositionB;
        PositionB = temp;
        currentPosition = PositionA.position;
        targetedPosition = PositionB.position;
    }


    private void OnTouch(Vector3 currentPosition, Vector3 targetedPosition)
    {


        rb.MovePosition(Vector3.MoveTowards(currentPosition, targetedPosition, speed * Time.deltaTime));

    }

    private float CalculateTime(float velocity, float distance)
    {
        return distance / velocity;
    }

    private void ActivateElevator()
    {
        isMoving = false;
        Permute();
    }
}
