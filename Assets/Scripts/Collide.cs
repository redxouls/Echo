using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    public float velocity = -6.0f;
    AudioSource MyAudioSource;
    public AudioClip FallingStones;
    public Transform Wall;
    float mvdst = 0;
    bool collapse = false;
    void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.tag == "Trap" && collapse == false)
        {
            collapse = true;
            MyAudioSource.PlayOneShot(FallingStones, 0.7F);
        }
        Debug.Log(collisionInfo.tag);
    }
    // Start is called before the first frame update
    void Start()
    {   
        MyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collapse && mvdst < 5){
            // Moves the wall downward at two units per second.
            Wall.Translate(0,velocity * Time.deltaTime,0);
            mvdst += Mathf.Abs(velocity * Time.deltaTime);
        }
    }
}
