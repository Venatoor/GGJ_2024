using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject canon;
    private Animator animator;
    private Character character;
    private Transform charTransform;
    public float scaleDownFactor;

    public void Start()
    {
        character = FindObjectOfType<Character>();
        scaleDownFactor = character.scaleDownFactor;
        animator = canon.GetComponent<Animator>();
        charTransform = character.GetComponent<Transform>();  // Assign the Transform component.
    }

    public void OnInteract(Collision collision)
    {
        Rigidbody characterRb = collision.rigidbody;
        character.SetInCanonTrue();
        MoveToObject(canon.transform.position);
        ScaleDownObject();
    }

    void OnCollisionEnter(Collision collision)
    {
        OnInteract(collision);
    }

    private void ScaleDownObject()
    {
        Vector3 newScale = charTransform.localScale * scaleDownFactor;
        charTransform.localScale = newScale;
    }

    private void MoveToObject(Vector3 position)
    {
        charTransform.position = position;
    }
}
