using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    
    public SoundWaveManager soundWaveManager;
    public float lifeSpan;
    // public float flyingTime; // how long can grenade fly without collide with object
    public int numberOfWave; // how many wave will be created after explosion
    // public float explosionInterval; // interval between each explosion wave
    public int countOfWave;

    // grenade wave parameters
    public float waveThickness; 
    public float waveLifeSpan;
    public float waveSpeed;

    // private float timer;
    // private bool exploding;
    // private Vector3 targetPos;

    // audio
    public AudioClip clip;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        // timer = 0f;
        Destroy(gameObject, lifeSpan);
        // exploding = false;
        countOfWave = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // timer += Time.deltaTime;
        // if (timer >= flyingTime)
        // {
        //     Explode();
        // }
        // if (targetPos != Vector3.zero)
        // {
        //     transform.position = targetPos;
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (countOfWave < numberOfWave)
        {
            countOfWave++;
            CreateWave();
        }
    }

    // void Explode()
    // {
    //     if (exploding)
    //     {
    //         return;
    //     }
    //     exploding = true;
    //     // targetPos = gameObject.transform.position;
    //     // gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //     // gameObject.GetComponent<Rigidbody>().useGravity = false;
    //     Destroy(gameObject, explosionTime);
    //     // for (int i = 0; i < numberOfWave; ++i)
    //     // {
    //     //     Invoke("CreateWave", i * explosionInterval);
    //     // }
    // }

    void CreateWave()
    {
        soundWaveManager.AddWave(thickness, lifeSpan, speed, 1, transform.position, WAVE_ATTRIBUTE.GRENADE);
        audioSource.PlayOneShot(clip, 0.7F);
    }
}
