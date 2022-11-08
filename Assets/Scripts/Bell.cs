using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    // public SoundWaveManager soundWaveManager;
    
    // public float soundWaveSetInterval = 0.2f;
    // public int soundWaveSetCount = 3;

    private AudioSource m_MyAudioSource;

    // Initialize the Bell and invoke functions periodically
    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();

        // Invode Ring function every 10 second, wait 1 second in the beginning
        InvokeRepeating("Ring", 1f, 10f);
    }

    void Update()
    {

    }

    // Play the bell sound and add a set of wave in the current position
    void Ring()
    {
        m_MyAudioSource.Stop();
        m_MyAudioSource.Play();
        // soundWaveManager.AddWaveSet(transform.position, soundWaveSetInterval, soundWaveSetCount, SoundWaveManager.WAVE_ATTRIBUTE.ENV);
    }
}
