using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{   
    public float minEchoInterval = 0.2f;
    public float echoLifeSpan = 5.0f;
    public GameObject prefab;

    public CharacterController controller;
    public float speed = 1.0f;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool moving;

    private float timer = 0.2f;
    private bool released = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position,  groundDis, groundMask); // Ground Check
        JumpAndGravity();
        Move();
        Echo();
    }

    private void JumpAndGravity() { // TODO: maybe can add jump ?
        if (isGrounded && velocity.y < 0f) { 
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moving = x != 0 || z != 0;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void Echo() {
        if (timer < minEchoInterval) {
            timer += Time.deltaTime;
        }
        if (timer >= minEchoInterval && moving) {
            GameObject echo = Instantiate(prefab, transform.position, Quaternion.identity);
            echo.SetActive(true);
            Destroy(echo, echoLifeSpan);
            timer = 0f;
        }
    }

}
