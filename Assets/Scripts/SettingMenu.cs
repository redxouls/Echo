using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public Slider mouseSensitivity;
    public Slider volume;
    [SerializeField] private AudioMixer audioMixer;
    public TMP_Dropdown resDropdown;
    private Resolution[] resolutions;
    private bool created = false;
    private int initialResIdx;

    void Start() 
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> options = new List<string>();

        int curResIdx = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                curResIdx = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = curResIdx;
        resDropdown.RefreshShownValue();
        if (PlayerPrefs.GetInt("Created") != 1)
        {
            ResetSetting();
            PlayerPrefs.SetInt("Created", 1);
            Debug.Log("first time setting.");
        }
        else
        {
            LoadSetting();
        }
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        if (ThirdPersonCameraManager.Instance != null)
        {
            ThirdPersonCameraManager.Instance.UpdateMouseSensitivity(mouseSensitivity);
        }
    }

    public void SetResolution(int curResIdx)
    {
        Resolution resolution = resolutions[curResIdx];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void LoadSetting()
    {
        // Debug.Log(mouseSensitivity.value);
        float _mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        float _volume = PlayerPrefs.GetFloat("Volume");
        SetMouseSensitivity(_mouseSensitivity);
        SetVolume(_volume);
    }

    public void ResetSetting()
    {
        mouseSensitivity.value = 0.5f;
        volume.value = 1;
        SetMouseSensitivity(mouseSensitivity.value);
        SetVolume(volume.value);
    }
}