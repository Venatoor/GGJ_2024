using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowInteractible : MonoBehaviour, IInteractable, ISpawnable
{
    private Color color;
    private float size;
    private Renderer objectRenderer;
    private bool isRendered;

    [SerializeField]
    private float slowAmount;
    [SerializeField]
    private float slowDuration;
    PlayerLocomotion playerLocomotion;

    [SerializeField]
    private AudioSource slowSFX;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        isRendered = true; 
    }
    public void OnInteract(Collider collision)
    {

        Character character = collision.GetComponent<Character>();
        playerLocomotion = character.GetComponent<PlayerLocomotion>();
        if (character != null)
        {
            if (isRendered)
            {

                //checking if slow amount is correctly clamped
                AudioManager.instance.playAudio(slowSFX);
                float slowAmountClamped = Mathf.Clamp01(slowAmount);
                playerLocomotion.SetSpeedFactor(slowAmountClamped);
                StartCoroutine(SpeedReset());

            }
        }
    }

    public void OnSpawn()
    {

    }

    private IEnumerator SpeedReset()
    {
        yield return new WaitForSeconds(slowDuration);
        playerLocomotion.SetSpeedFactor(0);

    }

    private void OnTriggerEnter(Collider collision)
    {
        OnInteract(collision);
    }
}
