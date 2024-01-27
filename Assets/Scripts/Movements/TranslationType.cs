using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TranslationType
{
    TranslationX, TranslationY, TranslationXY, TranslationOscillatingX, TranslationOscillatingY, TranslationElliptic, TranslationCircular, TranslationMoveTowards
}
public class TranslationMovement : MonoBehaviour, IMovement
{


    [SerializeField]
    TranslationType translationType;

    private static TranslationMovement instance;
    public static TranslationMovement Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("TranslationMovement");
                obj.AddComponent<TranslationMovement>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public IEnumerator DoTranslationX(Transform target, float speed, Vector3 direction, float step)
    {
        for (int i = 0; i < step; i++)
        {
            Vector3 velocity = new Vector3(0, 0, 0);
            velocity.x = speed * Time.deltaTime;
            target.Translate(new Vector3(direction.x * velocity.x, direction.y * velocity.y), Space.World);
            yield return null;
        }
    }



    public IEnumerator DoTranslationY(Transform target, float speed, Vector3 direction)
    {
        Vector3 velocity = new Vector3(0, 0, 0);
        velocity.y = speed * Time.deltaTime;
        target.Translate(new Vector2(direction.x * velocity.x, direction.y * velocity.y), Space.World);
        yield return null;
    }

    public IEnumerator DoTranslationXY(float speed, Vector3 direction)
    {
        Vector3 velocity = new Vector3(0, 0, 0);
        velocity.x = speed * Time.deltaTime;
        velocity.y = speed * Time.deltaTime;
        transform.Translate(new Vector3(direction.x * velocity.x, direction.y * velocity.y), Space.World);
        yield return null;
    }

    public void SetTranslationType(TranslationType translationType)
    {
        this.translationType = translationType;
    }

    public IEnumerator DoTranslationOscillatingX(float frequency, float amplitude)
    {
        Vector3 velocity = new Vector3(0, 0, 0);
        velocity.x = Mathf.Sin(frequency * Time.time) * amplitude;
        transform.Translate(new Vector3(velocity.x, velocity.y, velocity.z));
        yield return null;
    }

    public IEnumerator DoTranslationOscillatingY(float frequency, float amplitude)
    {
        Vector3 velocity = new Vector3(0, 0, 0);
        velocity.y = Mathf.Sin(frequency * Time.time) * amplitude;
        transform.Translate(new Vector3(velocity.x, velocity.y, velocity.z));
        yield return null;
    }

    public IEnumerator DoTranslationCircular(float frequency, float amplitude)
    {
        Vector3 velocity = new Vector3(0, 0);
        velocity.x = Mathf.Sin(frequency * Time.time) * amplitude;
        velocity.y = Mathf.Cos(frequency * Time.time) * amplitude;
        transform.Translate(new Vector3(velocity.x, velocity.y));
        yield return null;
    }


    public IEnumerator DoFixedLerpingTranslation(Transform transform, float duration, Transform beginPosition, Transform endPosition)
    {
        float timeElapsed = 0f;
        Vector3 destination = endPosition.position;
        while (timeElapsed < duration)
        {
            transform.position = Vector2.Lerp(beginPosition.position, destination, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

    }

    //OTHER TYPES OF LERPING 

    public IEnumerator DoLerpingTranslation()
    {
        yield return null;
    }

    //COMPOSITE MOVEMENTS WHERE COROUTINES ARE STARTED INSIDE COROUTINES

    public IEnumerator DoSemiHomingTranslation()
    {
        yield return null;
    }

    public IEnumerator DoHomingTranslation()
    {
        yield return null;
    }
}
