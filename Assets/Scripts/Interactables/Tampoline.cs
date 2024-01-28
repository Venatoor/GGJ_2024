using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tampoline : MonoBehaviour 
{
    public GameObject trampoline;
    private Animator animator;

    [SerializeField]
    private AudioSource tampolineSFX;


    [SerializeField]
    private float forceAmountX;

    [SerializeField]
    private float forceAmountY;

    [SerializeField]
    private float forceAmountZ;

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
        AudioManager.instance.playAudio(tampolineSFX);
        Vector3 trampolineForce = new Vector3(forceAmountX, forceAmountY, forceAmountZ);
        playerRigidbody.AddForce(trampolineForce, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnInteract(collision);
    }

}
