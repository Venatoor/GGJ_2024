using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tampoline : MonoBehaviour 
{
    public void OnInteract(Collision collision)
    {
        Debug.Log("heee");
        Rigidbody characterRb = collision.rigidbody;
        ApplyTrampolineForce(characterRb);

    }
    void ApplyTrampolineForce(Rigidbody playerRigidbody)
    {
        Vector3 trampolineForce = new Vector3(0f, 1000f, 0f);
        playerRigidbody.AddForce(trampolineForce, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnInteract(collision);
    }

}
