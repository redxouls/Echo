using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ThirdPersonCameraManager : MonoBehaviour
{
    public CinemachineFreeLook freelookCamera;
    public float mouseSensitivity;
    public float baseXAxisSpeed;
    public float baseYAxisSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.lockState = CursorLockMode.Locked;
        // mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        freelookCamera.m_XAxis.m_MaxSpeed = baseXAxisSpeed * mouseSensitivity;
        freelookCamera.m_YAxis.m_MaxSpeed = baseYAxisSpeed * mouseSensitivity;
    }

    public void UpdateMouseSensitivity(float mouseSensitivity)
    {
        this.mouseSensitivity = mouseSensitivity;
    }
}
