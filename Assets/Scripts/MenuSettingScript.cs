using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSettingScript : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider masterSliders;
    public Slider musicSlider;
    public Slider sfxSliders;

    void OnEnable()
    {
        LoadVolume();
    }

    void LoadVolume()
    {
        SetSliderFromMixer("MasterVolume", masterSliders);
        SetSliderFromMixer("MusicVolume", musicSlider);
        SetSliderFromMixer("SfxVolume", sfxSliders);
    }

    void SetSliderFromMixer(string param, Slider slider)
    {
        if (audioMixer.GetFloat(param, out float volumeDb))
        {
            slider.SetValueWithoutNotify(DbToLinear(volumeDb));
        }
    }

    float DbToLinear(float db)
    {
        return Mathf.Pow(10f, db / 20f);
    }
    public void returnMainMenu()
    {
        gameObject.SetActive(false);
    }
}
