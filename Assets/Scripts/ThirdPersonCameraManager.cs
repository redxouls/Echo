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
        Cursor.visible = false;
        Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        freelookCamera.m_XAxis.m_MaxSpeed = baseXAxisSpeed * mouseSensitivity;
        freelookCamera.m_YAxis.m_MaxSpeed = baseYAxisSpeed * mouseSensitivity;
    }
}
