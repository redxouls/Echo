using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static SoundWaveManager soundWaveManager;

    public void SetWaveSpeed(float waveSpeed) {
        // soundWaveManager.SetFloat("waveSpeed", waveSpeed);
        PlayerPrefs.SetFloat("waveSpeed", waveSpeed);
    }
    public void SetWaveDuration(float waveDuration) {
        Debug.Log(waveDuration);
        PlayerPrefs.SetFloat("echoLifeSpan", waveDuration);
    }
    public void SetWaveInterval(float waveInterval) {
        PlayerPrefs.SetFloat("minEchoInterval", waveInterval);
    }
    public void SetPlayerSpeed(float playerSpeed) {
        PlayerPrefs.SetFloat("speed", playerSpeed);
    }
    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetWaveWidth(float waveWidth) {
        PlayerPrefs.SetFloat("thickness", waveWidth);
    }

    void Start() {
        PlayerPrefs.SetFloat("waveSpeed", 5);
        PlayerPrefs.SetFloat("echoLifeSpan", 5);
        PlayerPrefs.SetFloat("minEchoInterval", 1);
        PlayerPrefs.SetFloat("speed", 1);
        PlayerPrefs.SetFloat("Volume", 5);
        PlayerPrefs.SetFloat("thikness", 0.3f);
    }

}
