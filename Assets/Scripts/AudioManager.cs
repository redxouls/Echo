using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance{get; private set; }
    public AudioSource MyAudioSource;
    public AudioClip ClickButton;

    void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    public void PlayAudioClip(AudioClip audioClip, string type) 
    {
        if (type=="footstep")
        {
            MyAudioSource.volume = Random.Range(0.7f,1f);
            MyAudioSource.pitch = Random.Range(0.8f,1.1f);
        }
        
        MyAudioSource.PlayOneShot(audioClip);
    }

    public void PlayClickButton()
    {
        MyAudioSource.PlayOneShot(ClickButton);
    }

}
