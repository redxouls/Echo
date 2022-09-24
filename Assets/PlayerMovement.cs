using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{   
    public float speed = 1.0f;
    public GameObject prefab;
    public CharacterController controller;

    private float minEchoInterval = 0.2f;
    private float timer = 0.2f;
    private float echoLifeSpan = 5.0f;
    private bool released = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (timer < minEchoInterval) {
            timer += Time.deltaTime;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool moving = z !=0 || z != 0;
        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * speed * Time.deltaTime);

        if (timer >= minEchoInterval && moving) {
            Echo();
            timer = 0f;
        }
        
    }

    void Echo() {
        GameObject echo = Instantiate(prefab, transform.position, Quaternion.identity);
        echo.SetActive(true);
        Destroy(echo, echoLifeSpan);
    }
}
