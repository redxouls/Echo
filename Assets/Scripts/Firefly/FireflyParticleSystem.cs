using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyParticleSystem : MonoBehaviour
{   
    private new ParticleSystem particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        var shape = particleSystem.shape;
        
        shape.radius = 5.0f + 5.0f *Mathf.Sin(time/5);
        // Debug.Log(shape.radius);
    }
}
