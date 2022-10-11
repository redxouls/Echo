using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTracing : MonoBehaviour
{
    public SoundWaveManager soundWaveManager;
    public float speed;
    public CharacterController controller;
    public Vector3 target; // set to public for the convenience of debug

    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Trace", 1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.Normalize(target - transform.position) * speed * Time.deltaTime;
        move.y = 0;
        transform.position = transform.position + move;
        //controller.Move(move);
        
    }
    void Trace() {
        int start = soundWaveManager.startIndex;
        int end = soundWaveManager.endIndex;
        int len = soundWaveManager.maxNumOfWave;
        float targetDistance = float.MaxValue;
        for (int i = 0; i < (end + len - start) % len; ++i) {
            int index = (start + i) % len;
            float dist = Vector3.Distance(soundWaveManager.points[index], transform.position); // Unity would auto cast Vector4 to Vector3 by use the first three values
            if (Mathf.Abs(dist - soundWaveManager.points[index].w) < 1) {
                if (dist < targetDistance) {
                    targetDistance = dist;
                    target = soundWaveManager.points[index];
                }
            }
        }
    }
}
