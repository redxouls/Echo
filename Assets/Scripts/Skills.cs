using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public float minLifeSpan;
    public float maxLifeSpan;
    public float waveThickness;
    public float waveSpeed;    
    public float waveLifespan;
    
    public SoundWaveManager soundWaveManager;
    
    private float pressedDuration;
    private Transform Trail;
    // Start is called before the first frame update
    void Start()
    {
        Trail = transform.Find("Trail");
        waveThickness = PlayerPrefs.GetFloat("waveThickness");
        waveSpeed = PlayerPrefs.GetFloat("waveSpeed");
        waveLifespan = PlayerPrefs.GetFloat("waveLifespan");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseController.GamePaused)
        {
            Clap();
        }
    }

    void Clap()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressedDuration = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pressedDuration = Time.time - pressedDuration;

            float factor = Mathf.Min(Mathf.Max(pressedDuration, minLifeSpan), maxLifeSpan) / maxLifeSpan;
            float lifeSpan, thickness, speed;
            
            // Small wave: search area arround the player (near)
            // if (factor < 0.45f)
            // {
            //     lifeSpan = factor * 3.5f;
            //     thickness = 0.4f;
            //     speed = 0.65f;
            //     Debug.Log("Small Wave");
            // }

            // Medium wave: search area arround the player (far)
            // if (factor < 0.75f)
            // {
            //     // Debug.Log("Medium Wave");
            //     lifeSpan = factor * 10f;
            //     thickness = 2f;
            //     speed = 2.5f;
            // }

            // Large wave: obeserve the entire environment
            // else
            // {
            //     // Debug.Log("Large Wave");
            //     lifeSpan = factor * 20f;
            //     thickness = 4f;
            //     speed = 3f;
            // }

            // soundWaveManager.AddWave(thickness, lifeSpan, speed, 1, Trail.position, WAVE_ATTRIBUTE.PLAYER);
            soundWaveManager.AddWave(waveThickness, waveLifespan, waveSpeed, 1, Trail.position, WAVE_ATTRIBUTE.PLAYER);
        }
    }
}