using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);

    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20f);

    }

    public void SetPreviousVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float previousVolume);
        PlayerPrefs.SetFloat("MusicVolume", previousVolume);
        audioMixer.GetFloat("SfxVolume", out float previousSfxVolume);
        PlayerPrefs.SetFloat("SfxVolume", previousSfxVolume);
        audioMixer.GetFloat("MasterVolume", out float previousMasterVolume);
        PlayerPrefs.SetFloat("MasterVolume", previousMasterVolume);
    }

    public void LoadPreviousVolumes()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0f);

        audioMixer.SetFloat("MasterVolume", masterVolume);
        audioMixer.SetFloat("MusicVolume", musicVolume);
        audioMixer.SetFloat("SfxVolume", sfxVolume);
    }
}
