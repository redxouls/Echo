using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public float velocity = -2.0f;
    public AudioSource MyAudioSource;
    public Transform Wall1;
    public Transform Wall2;
    float mvdst = 0;
    bool triggered = false;
    void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.tag == "Player")
        {
            triggered = true;
            MyAudioSource.Play();
        }
        // Debug.Log(collisionInfo.tag);
    }
    // Start is called before the first frame update
    void Start()
    {   
        MyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered && mvdst < 5){
            // Moves the wall downward at two units per second.
            Wall1.Translate(0,velocity * Time.deltaTime,0);
            Wall2.Translate(0,velocity * Time.deltaTime,0);
            mvdst += Mathf.Abs(velocity * Time.deltaTime);
        }
    }
}
