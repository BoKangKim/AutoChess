using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider[] soundSlider; // MASTER, BGM, SFX

    public void SetMasterVolume() 
    {
        audioMixer.SetFloat("MASTER", Mathf.Log10(soundSlider[0].value) * 20f);
    }
    public void SetBgmVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(soundSlider[1].value) * 20f);
    }
    public void SetSfxVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(soundSlider[2].value) * 20f);
    }
}
