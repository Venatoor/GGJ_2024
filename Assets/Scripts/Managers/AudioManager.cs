using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer effectsMixer;
    public static AudioManager instance;


    [Range(-80, 10)]
    public float masterVol, effectsVol;
    public Slider masterSldr, effectsSldr;
    // Start is called before the first frame update


    private void Start()
    {

        masterSldr.minValue = -80;
        masterSldr.maxValue = 10;
        effectsSldr.minValue = -80;
        effectsSldr.maxValue = 10;

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void playAudio(AudioSource audio)
    {
        audio.Play();
    }

}
