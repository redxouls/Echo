using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    
    public SoundWaveManager soundWaveManager;
    public float explosionTime; // how much time will take in the whole explosion (lighting up the world)
    public float flyingTime; // how long can grenade fly without collide with object
    public int numberOfWave; // how many wave will be created after explosion
    public float explosionInterval; // interval between each explosion wave

    // grenade wave parameters
    public float thickness; 
    public float lifeSpan;
    public float speed;

    private float timer;
    private bool exploding;
    private Vector3 targetPos;

    // audio
    public AudioClip clip;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        exploding = false;
        targetPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        timer += Time.deltaTime;
        if (timer >= flyingTime)
        {
            Explode();
        }
        if (targetPos != Vector3.zero)
        {
            transform.position = targetPos;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        if (exploding)
        {
            return;
        }
        exploding = true;
        targetPos = gameObject.transform.position;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Destroy(gameObject, explosionTime);
        for (int i = 0; i < numberOfWave; ++i)
        {
            Invoke("CreateWave", i * explosionInterval);
        }
    }

    void CreateWave()
    {
        soundWaveManager.AddWave(thickness, lifeSpan, speed, transform.position, WAVE_ATTRIBUTE.GRENADE);
        audioSource.PlayOneShot(clip, 0.7F);
    }
}
