using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider mouseSensitivity;
    public Slider volume;
    [SerializeField] private AudioMixer audioMixer;
    bool created = false;

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMouseSensitivity(float waveWidth)
    {
        PlayerPrefs.SetFloat("waveThickness", waveWidth);
    }

    public void DefaultSetting()
    {
        mouseSensitivity.value = 100;
        volume.value = 1;
    }

    void Start() 
    {
       DefaultSetting(); 
    }

}
