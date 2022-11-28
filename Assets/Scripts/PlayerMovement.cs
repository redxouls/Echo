using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{   
    public GameObject prefab;
    public CharacterController controller;
    public float speed;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;
    public Transform camera;

    public float minEchoInterval;
    public float soundWaveSetInterval;
    public int soundWaveSetCount;
    public SoundWaveManager soundWaveManager;

    public AudioSource MyAudioSource;

    private Vector3 velocity;
    private bool isGrounded;
    private bool moving;

    private float timer = 0.2f;

    public float waveThickness;
    public float waveSpeed;
    public float waveLifespan;

    private bool isDead = false;
    public AudioClip woodSteps;
    public AudioClip deathSound;
    public GameObject deathScreen;
    // private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultipler : IsSprinting ? baseStepSpeed * sprintStepMultipler : baseStepSpeed;
    // Start is called before the first frame update
    void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
        //Load Player Setting
        speed = PlayerPrefs.GetFloat("playerSpeed");
        waveThickness = PlayerPrefs.GetFloat("waveThickness");
        waveSpeed = PlayerPrefs.GetFloat("waveSpeed");
        waveLifespan = PlayerPrefs.GetFloat("waveLifespan");
        minEchoInterval = PlayerPrefs.GetFloat("minEchoInterval");
    }

    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask); // Ground Check
        JumpAndGravity();
        Move();
        HandleFootsteps();
        // Echo();
    }
    void LateUpdate()
    {
        if(isDead)
        {
            timer = 0;
            var color = deathScreen.GetComponent<Image>().color;
            if(color.a < 0.8f)
            {
                color.a += 1f * Time.deltaTime;
            }
            deathScreen.GetComponent<Image>().color = color;
        }
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
        transform.rotation = Quaternion.Euler(0, camera.eulerAngles.y, 0);
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
            soundWaveManager.AddWave(waveThickness, waveLifespan, waveSpeed, 1, transform.position, WAVE_ATTRIBUTE.PLAYER);
            // soundWaveManager.AddPlayerWave(transform.position);
            // Add a sound source upon moving
            // soundWaveManager.AddWaveSource(transform.position);
            // soundWaveManager.AddWaveSet(transform.position, soundWaveSetInterval, soundWaveSetCount, SoundWaveManager.WAVE_ATTRIBUTE.PLAYER);
            // GameObject echo = Instantiate(prefab, transform.position, Quaternion.identity);
            // echo.SetActive(true);
            // Destroy(echo, echoLifeSpan);
            // MyAudioSource.Play();
            timer = 0f;
        }
    }

    private void HandleFootsteps()
    {
        if (timer >= minEchoInterval - Time.deltaTime && moving)
        {
            if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                // Debug.Log(hit.collider.tag);
                switch (hit.collider.tag)
                {
                    case "Footsteps/WOOD":
                        MyAudioSource.PlayOneShot(woodSteps);
                        break;
                    default:
                        MyAudioSource.Play();
                        break;
                }
            }
            else
            {
                MyAudioSource.Play();
            }
        }
    }
    // Player Death by hitting trap or evil, stop moving and show deathScreen
    void OnTriggerEnter(Collider collisionInfo)
    {
        switch (collisionInfo.tag)
        {
            case "Trap":
                speed = 0;
                MyAudioSource.PlayOneShot(deathSound);
                isDead = true;
                break;
            case "Destination":
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
                #if UNITY_STANDALONE
                Application.Quit();
                #endif
                break;
        }
        // Debug.Log(collisionInfo.tag);
    }
}
