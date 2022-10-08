using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{   
    public GameObject prefab;
    public CharacterController controller;
    public float speed = 1.0f;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;

    public float minEchoInterval;
    public float soundWaveSetInterval;
    public int soundWaveSetCount;
    public SoundWaveManager soundWaveManager;

    private Vector3 velocity;
    private bool isGrounded;
    private bool moving;

    private float timer = 0.2f;

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
    
    // TODO: maybe can add jump ?
    private void JumpAndGravity()
    { 
        if (isGrounded && velocity.y < 0f)
        { 
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moving = x != 0 || z != 0;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void Echo()
    {
        if (timer < minEchoInterval) 
        {
            timer += Time.deltaTime;
        }
        if (timer >= minEchoInterval && moving) 
        {
            // Add a sound source upon moving
            //soundWaveManager.AddWaveSource(transform.position);
            soundWaveManager.AddWaveSet(transform.position, soundWaveSetInterval, soundWaveSetCount);
            // GameObject echo = Instantiate(prefab, transform.position, Quaternion.identity);
            // echo.SetActive(true);
            // Destroy(echo, echoLifeSpan);
            timer = 0f;
        }
    }

}
