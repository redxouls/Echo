using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public float lookXLimit = 45.0f;

    public Transform player;

    float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        Debug.Log("Mouse");
        Debug.Log(mouseSensitivity);
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input and adjust with mouseSensitivity
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate rotation for x-axis
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);

        // Set calculate values to player and camera
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
