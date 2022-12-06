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
    public ThirdPersonCameraManager thirdPersonCameraManager;
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
        LoadSetting();
        PlayerPrefs.SetInt("Created", 1);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        thirdPersonCameraManager.UpdateMouseSensitivity(mouseSensitivity);
    }

    public void SetResolution(int curResIdx)
    {
        Resolution resolution = resolutions[curResIdx];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void LoadSetting()
    {
        Debug.Log(mouseSensitivity.value);
        SetMouseSensitivity(mouseSensitivity.value);
        SetVolume(volume.value);
    }

    public void ResetSetting()
    {
        mouseSensitivity.value = 0.5f;
        volume.value = 1;
        SetMouseSensitivity(mouseSensitivity.value);
        SetVolume(volume.value);
    }
}