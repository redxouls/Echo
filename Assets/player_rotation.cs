using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_rotation : MonoBehaviour
{
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
    }
}
