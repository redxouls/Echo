using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    
    public SoundWaveManager soundWaveManager;
    public float explosionTime; // how much time will take in the whole explosion (lighting up the world)
    public float flyingTime; // how long can grenade fly without collide with object
    public int numberOfWave; // how many wave will be created after explosion

    private float timer;
    private bool exploding;
    private Vector3 targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        exploding = false;
        targetPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
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
            Invoke("CreateWave", i * (explosionTime / numberOfWave));
        }
    }

    void CreateWave()
    {
        soundWaveManager.AddGrenadeWave(transform.position);
    }
}
