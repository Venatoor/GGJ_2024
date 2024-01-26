using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour, IInteractable, ISpawnable
{
    private Color color;
    private float size;
    private Renderer objectRenderer;
    private bool isRendered;

    [SerializeField]
    private float amountToGive;
    // Start is called before the first frame update
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    public void OnInteract(Collider collision)
    {
        Character character = collision.GetComponent<Character>();
        if (character != null)
        {      
            if (isRendered)
            {
                LaughStat laughStat = character.GetComponent<LaughStat>();
                laughStat.Increase(amountToGive);

                objectRenderer.enabled = false;
                isRendered = false;

                //Destruction Effect


            }
        }
    }

    public void OnSpawn()
    {
        //Playing sfx sound
    }

    private void OnTriggerEnter(Collider collision)
    {
        OnInteract(collision);
    }



}
