using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public SoundWaveManager soundWaveManager;
    
    private float lifeSpan;
    private Transform Trail;
    // Start is called before the first frame update
    void Start()
    {
        Trail = transform.Find("Trail");
    }

    // Update is called once per frame
    void Update()
    {
        Clap();
    }

    void Clap()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lifeSpan = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            lifeSpan = Time.time - lifeSpan;
            float strength =  Mathf.Min(lifeSpan, 2f) / 2f;
            
            float thickness = strength * 1.5f + 1f;
            lifeSpan = strength * 5f + 1f;
            float speed = strength * 1.5f + 0.5f;
            
            soundWaveManager.AddWave(thickness, lifeSpan, speed, Trail.position, WAVE_ATTRIBUTE.PLAYER);
        }
    }
}