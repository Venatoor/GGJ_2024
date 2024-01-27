using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Character>() == true)
        {
            collision.collider.transform.SetParent(gameObject.transform);
        }
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Character>() == true)
        {
            collision.collider.transform.SetParent(null);
        }
    }



    public virtual void OnTouch() { }


}
