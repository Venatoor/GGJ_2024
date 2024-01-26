using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


    [SerializeField] public Image _img;
    [SerializeField] public AudioClip _compresseClip, _uncompressClip;
    [SerializeField] public AudioSource _source;

    public virtual  void OnPointerDown(PointerEventData eventData)
    {
        //_source.PlayOneShot(_compresseClip);
    }

    public virtual  void OnPointerUp(PointerEventData eventData)
    {
        //_source.PlayOneShot(_uncompressClip);
    }

    public void ChangeScene()
    {
        // insert scene name
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
