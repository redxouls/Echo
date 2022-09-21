using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{   
    public float step_size = 1.0f;
    public float rotation = 0.2f;
    private float y = 0.0f;
    private float z = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) {
            this.transform.localPosition += new Vector3(-step_size, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            this.transform.localPosition += new Vector3(0, 0, -step_size) * Time.deltaTime;
        }
        if (Input.GetKey("s")) {
            this.transform.localPosition += new Vector3(step_size, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            this.transform.localPosition += new Vector3(0, 0, step_size) * Time.deltaTime;
        }
        if (Input.GetKey("left")) {
            y -= rotation;
        }
        if (Input.GetKey("right")) {
            y += rotation;
        }
        if (Input.GetKey("up")) {
            z -= rotation;
        }
        if (Input.GetKey("down")) {
            z += rotation;
        }
        this.transform.localRotation = Quaternion.Euler(0, y, z);
        // Debug.Log(this.transform.localPosition);
    }
}
