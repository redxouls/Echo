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

    public static ThirdPersonCameraManager Instance { get; private set; }
    void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
        

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");    // No singleton case
        freelookCamera.m_XAxis.m_MaxSpeed = baseXAxisSpeed * mouseSensitivity;
        freelookCamera.m_YAxis.m_MaxSpeed = baseYAxisSpeed * mouseSensitivity;
    }

    public void UpdateMouseSensitivity(float new_mouseSensitivity)
    {
        mouseSensitivity = new_mouseSensitivity;
    }
}
