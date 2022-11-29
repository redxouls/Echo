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
    // bool created = false;

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
    }

    public void DefaultSetting()
    {
        mouseSensitivity.value = 0.5f;
        volume.value = 1;
    }

    void Start() 
    {
       DefaultSetting(); 
    }

}
