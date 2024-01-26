
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ToggleButton : ClickableButton
{
    private bool toggle = true;
    [SerializeField] public Sprite _default, _pressed;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (toggle)
        {
            _img.sprite = _pressed;
            //_source.PlayOneShot(_compresseClip);
            toggle = false;
        }
        else
        { _img.sprite = _default; toggle = true; }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //_source.PlayOneShot(_uncompressClip);
    }
}
