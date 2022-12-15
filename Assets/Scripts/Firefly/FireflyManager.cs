using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyManager : MonoBehaviour
{   

    private FireflyLight fireflyLight;
    private FireflyParticleSystem fireflyParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        fireflyLight = transform.GetChild(0).GetComponent<Light>().GetComponent<FireflyLight>();
        fireflyParticleSystem = transform.GetChild(1).GetComponent<ParticleSystem>().GetComponent<FireflyParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        fireflyParticleSystem.state = FireflyParticleSystem.State.Shrink;
        Debug.Log(other);
        // transform.position = transform.position + new Vector3(2.0f, 0.0f, 2.0f);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other);
        // fireflyParticleSystem.state = FireflyParticleSystem.State.Spread;
    }
}
