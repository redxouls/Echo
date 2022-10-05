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

    public Material postProcessingMaterial;
    public Vector4[] points;
    public int startIndex;
    public int endIndex;
    public float waveSpeed = 20.0f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool moving;

    private float timer = 0.2f;
    private bool released = false;

    // Start is called before the first frame update
    void Start()
    {
        startIndex = 0;
        endIndex = 0;
        points = new Vector4[100];
    }

    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position,  groundDis, groundMask); // Ground Check
        JumpAndGravity();
        Move();
        Echo();
        UpdateWaveSource();
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

    // Add a wave source to point and increase endIndex
    private void AddWaveSource()
    {
        points[endIndex] = new Vector4(transform.position.x, transform.position.y, transform.position.z, 0);
        endIndex = (endIndex + 1) % points.Length;
    }

    // Update wave radius and remove wave older than life span;
    private void UpdateWaveSource()
    {
        int newStartIndex = startIndex;
        for (int i = 0; i < (endIndex + points.Length - startIndex) % points.Length; i++) 
        {
            int index = (startIndex + i) % points.Length;
            points[index].w += Time.deltaTime * waveSpeed;
            if (points[index].w > echoLifeSpan * waveSpeed)
            {
                newStartIndex ++;
            }
        }
        startIndex = newStartIndex;
        postProcessingMaterial.SetInt("_StartIndex", startIndex);
        postProcessingMaterial.SetInt("_EndIndex", endIndex);
        postProcessingMaterial.SetVectorArray("_Points", points);
    }

    private void Echo()
    {
        if (timer < minEchoInterval) 
        {
            timer += Time.deltaTime;
        }
        if (timer >= minEchoInterval && moving) 
        {
            // Add a remove source upon moving
            AddWaveSource();
            // GameObject echo = Instantiate(prefab, transform.position, Quaternion.identity);
            // echo.SetActive(true);
            // Destroy(echo, echoLifeSpan);
            timer = 0f;
        }
    }

}
