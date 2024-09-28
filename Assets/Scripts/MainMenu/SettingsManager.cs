using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer MixerSFX;
    [SerializeField] private AudioMixer MixerVolume;


    public void OnSFXValueChanged(float newValue)
    {
        if(newValue < 0.01f)
        {
            newValue = 0.01f;
        }

        float volume = Mathf.Log10(newValue) * 20;
        PlayerPrefs.SetFloat("SFX_Volume", newValue);
        MixerSFX.SetFloat("SFX_Volume", volume);
    }

    public void OnVolumeChanged(float newValue)
    {
        if(newValue < 0.01f)
        {
            newValue = 0.01f;
        }
        float volume = Mathf.Log10(newValue) * 20;
        PlayerPrefs.SetFloat("Volume_Volume", newValue);
        MixerVolume.SetFloat("Volume_Volume", volume);
    }
}
