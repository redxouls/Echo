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
    public float groundDis = 0.5f;
    public LayerMask groundMask;
    public Transform camera;

    public float minEchoInterval;
    public float soundWaveSetInterval;
    public int soundWaveSetCount;
    public SoundWaveManager soundWaveManager;

    // public AudioSource MyAudioSource;

    private Vector3 velocity;
    private bool isGrounded;
    private bool moving;
    private bool WhichFoot;
    private Vector3 LastFootprint;
    public float FootprintSpacer = 1.0f;
    public GameObject LeftFoorPrefab = null;
    public GameObject RightFootPrefab = null;

    public float waveThickness;
    public float waveSpeed;    public float waveLifespan;

    public bool isDead = false;
    public GameObject deathBG;

    public AudioClip GrassSteps;
    public AudioClip WaterSteps;
    public AudioClip DeathSound;
    public AudioClip GroundSteps;
    public GameObject deathScreen;

    // private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultipler : IsSprinting ? baseStepSpeed * sprintStepMultipler : baseStepSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // MyAudioSource = GetComponent<AudioSource>();
        //Load Player Setting
        // speed = PlayerPrefs.GetFloat("playerSpeed");
        // waveThickness = PlayerPrefs.GetFloat("waveThickness");
        // waveSpeed = PlayerPrefs.GetFloat("waveSpeed");
        // waveLifespan = PlayerPrefs.GetFloat("waveLifespan");
        // minEchoInterval = PlayerPrefs.GetFloat("minEchoInterval");
        if (deathScreen)
            deathScreen.SetActive(isDead);
    }

    // Update is called once per frame
    void Update()
    {   
        if (!PauseController.GamePaused)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask); // Ground Check
            JumpAndGravity();
            Move();
            HandleFootsteps();
        }
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     isDead = !isDead;
        //     Cursor.visible = true;
        //     Cursor.lockState = CursorLockMode.None;
        // }
    }
    void LateUpdate()
    {
        if (isDead)
        {
            // Debug.Log("You dead.");
            deathScreen.SetActive(isDead);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            var color = deathBG.GetComponent<Image>().color;
            if(color.a < 0.8f)
            {
                color.a += 1f * Time.deltaTime;
            }
            deathBG.GetComponent<Image>().color = color;
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

    private void HandleFootsteps()
    {
        // Debug.LogFormat("timer :{0} | minEchoInterval :{1} | Time.deltaTime:{2}",timer,minEchoInterval,Time.deltaTime);
        float DistanceSinceLastFootprint = Vector3.Distance(LastFootprint, this.transform.position);
        if (moving && isGrounded && DistanceSinceLastFootprint >= FootprintSpacer)
        {
            Color foot_color = Color.white;
            // Audio play according to ground type
            if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                // Debug.Log(hit.collider.tag);
                switch (hit.collider.tag)
                {
                    case "Footsteps/WATER":
                        AudioManager.Instance.PlayAudioClip(WaterSteps);
                        foot_color = Color.white;
                        // MyAudioSource.PlayOneShot(WaterSteps);
                        break;
                    case "Footsteps/GRASS":
                        AudioManager.Instance.PlayAudioClip(GrassSteps);
                        foot_color = Color.green;
                        // MyAudioSource.PlayOneShot(GrassSteps);
                        break;
                    default:
                        AudioManager.Instance.PlayAudioClip(GroundSteps);
                        foot_color = Color.white;
                        // MyAudioSource.Play();
                        break;
                }

                //where the ray hits the ground we will place a footprint
                GameObject decal = Instantiate(WhichFoot?LeftFoorPrefab:RightFootPrefab);
                decal.transform.position = hit.point + new Vector3(0.0f,0.1f,0.0f);
                //turn the footprint to match the direction the player is facing
                // decal.transform.Rotate(Vector3.up, transform.eulerAngles.y);
                decal.GetComponent<Renderer>().material.SetColor("_EmissionColor", foot_color);
                decal.transform.rotation = Quaternion.Euler(90, camera.eulerAngles.y, 0);
                LastFootprint = transform.position;
                WhichFoot = !WhichFoot;
            }
            else
            {
                AudioManager.Instance.PlayAudioClip(GroundSteps);
                // MyAudioSource.Play();
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
                AudioManager.Instance.PlayAudioClip(DeathSound);
                // MyAudioSource.PlayOneShot(DeathSound);
                isDead = true;
                break;
            case "Destination":
                break;
                // #if UNITY_EDITOR
                // UnityEditor.EditorApplication.isPlaying = false;
                // #endif
                // #if UNITY_STANDALONE
                // Application.Quit();
                // #endif
                // break;
        }
        // Debug.Log(collisionInfo.tag);
    }
}
