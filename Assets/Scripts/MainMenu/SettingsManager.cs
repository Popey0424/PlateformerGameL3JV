using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer MixerSFX;
    [SerializeField] private AudioMixer MixerVolume;
    private AudioSource Music;


    public void OnMusicValueChanged(float newValue)
    {
        Music.volume = newValue;

    }

    public void OnSFXValueChanged(float newValue)
    {
        if(newValue < 0.0001f)
        {
            newValue = 0.0001f;
        }


        float volume = Mathf.Log10(newValue) * 20;

        //PlayerPrefs.SetFloat("SFX_Volume", newValue);
        MixerSFX.SetFloat("SFX_Volume", volume);
    }

    public void OnVolumeValueChanged(float newValue)
    {
        if (newValue < 0.0001f)
        {
            newValue = 0.0001f;
        }

        float volume = Mathf.Log10(newValue) * 20;

        //PlayerPrefs.SetFloat("Volume_Volume", newValue);
        MixerVolume.SetFloat("Volume_Volume", volume);
    }


}
