using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTracing : MonoBehaviour
{
    /*
    public SoundWaveManager soundWaveManager;
    public float speed;
    public CharacterController controller;
    public Vector3 target; // set to public for the convenience of debug
    public bool moving;
    
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Trace", 1f, 0.1f);
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        VisualizeEvil();
    }
    void Trace() {
        int start = soundWaveManager.startIndex;
        int end = soundWaveManager.endIndex;
        int len = soundWaveManager.maxNumOfWave;
        float targetDistance = float.MaxValue;
        for (int i = 0; i < (end + len - start) % len; ++i)
        {
            int index = (start + i) % len;
            // ignore the sound wave created by environment
            if (soundWaveManager.waveAttributes[index] == SoundWaveManager.WAVE_ATTRIBUTE.ENV)
            {
                continue;
            }
            float dist = Vector3.Distance(soundWaveManager.points[index], transform.position); // Unity would auto cast Vector4 to Vector3 by use the first three values
            if (Mathf.Abs(dist - soundWaveManager.points[index].w) < 1)
            {
                // select the closest target
                if (dist < targetDistance)
                {
                    targetDistance = dist;
                    target = soundWaveManager.points[index]; // Unity would auto cast Vector4 to Vector3 by use the first three values
                }
            }
        }
    }
    void Move() {
        Vector3 move = Vector3.Normalize(target - transform.position) * speed * Time.deltaTime;
        move.y = 0;
        transform.position = transform.position + move;
        //controller.Move(move);
        
        // Use the distance between evil and target to check if evil is moving
        float dx = transform.position.x - target.x;
        float dz = transform.position.z - target.z;
        if (Mathf.Sqrt(dx*dx + dz*dz) < 0.5f)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }
    void VisualizeEvil() {
        if (moving)
        {
            particleSystem.Play();
            // Debug.Log("moving");
        }
        else
        {
            particleSystem.Stop();
            // Debug.Log("stop");
        }
    }
    */
}
