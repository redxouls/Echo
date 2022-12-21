using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
    public float timer;
    public AudioClip caveBgm;
    public AudioClip natureBgm;
    private AudioSource audio;
    private bool state; // 1 for natureBgm, 0 for caveBgm
    private bool faded;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        state = false;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
        {
            Debug.Log(hit.collider.tag);
            switch (hit.collider.tag)
            {
                case "Footsteps/CAVE":
                    if (state)
                    {
                        // StartFade(audio, 100, 0f);
                        audio.clip = caveBgm;
                        ChangeBgm();
                    }
                    break;
                default:
                    // MyAudioSource.Play();
                    if(!state)
                    {
                        // StartFade(audio, 100, 0f);
                        audio.clip = natureBgm;
                        ChangeBgm();
                    }
                    break;
            }
        }
        // if (faded)
        //     ChangeBgm();
    }
    void ChangeBgm()
    {
        audio.Play();
        audio.loop = true;
        faded = false;
        state = !state;
    }
    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        state = !state;
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            Debug.Log(currentTime);
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        faded = true;
        yield break;
    }
    // IEnumerator LoopAudio()
    // {
    //     audio.Play();
    //     yield return new WaitForSeconds(audio.clip.length);
    //     // while(true)
    //     // {  
            
    //     // }
    //     audio.clip = natureBgm;
    //     audio.Play();
    // }
}
