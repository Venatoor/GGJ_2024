using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tampoline : MonoBehaviour 
{
    public GameObject trampoline;
    private Animator animator;

    public void Start()
    {
        animator = trampoline.GetComponent<Animator>();
    }
    public void OnInteract(Collision collision)
    {
        
        Rigidbody characterRb = collision.rigidbody;
        ApplyTrampolineForce(characterRb);
        ApplyAnimation();

    }

    void ApplyAnimation() {
        
        animator.SetTrigger("JumpedOn");
    }
    void ApplyTrampolineForce(Rigidbody playerRigidbody)
    {
        Vector3 trampolineForce = new Vector3(0f, 700f, 0f);
        playerRigidbody.AddForce(trampolineForce, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnInteract(collision);
    }

}
