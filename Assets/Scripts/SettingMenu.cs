using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider waveSpeed;
    public Slider waveLifespan;
    public Slider minEchoInterval;
    public Slider playerSpeed;
    public Slider Volume;
    public Slider waveThickness;
    public AudioMixer audioMixer;
    public static SoundWaveManager soundWaveManager;
    bool created = false;
    public void SetWaveSpeed(float waveSpeed) {
        // soundWaveManager.SetFloat("waveSpeed", waveSpeed);
        PlayerPrefs.SetFloat("waveSpeed", waveSpeed);
    }
    public void SetWaveDuration(float waveLifespan) {
        Debug.Log(waveLifespan);
        PlayerPrefs.SetFloat("waveLifespan", waveLifespan);
    }
    public void SetWaveInterval(float waveInterval) {
        PlayerPrefs.SetFloat("minEchoInterval", waveInterval);
    }
    public void SetPlayerSpeed(float playerSpeed) {
        PlayerPrefs.SetFloat("playerSpeed", playerSpeed);
    }
    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetWaveWidth(float waveWidth) {
        PlayerPrefs.SetFloat("waveThickness", waveWidth);
    }

    public void DefaultSetting()
    {
        waveSpeed.value = 3;
        waveLifespan.value = 5;
        minEchoInterval.value = 2f;
        playerSpeed.value = 1.5f;
        Volume.value = 5;
        waveThickness.value = 2;
    }
    void Start() 
    {
        
    }

}
