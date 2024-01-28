using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPickUps : MonoBehaviour, IInteractable
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnInteract(Collider collision)
    {
        Character character = collision.GetComponent<Character>();
        if (character != null)
        {
            gameManager.IncrementNumberOfBallons();
            print(gameManager.currentBallons);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        OnInteract(collision);
    }

}
