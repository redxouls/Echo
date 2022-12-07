using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{   
    public float radius;
    public float deltaHeight;
    public float intensityMin = 0;
    public float intensityMax = 4;
    public float period = 1;
    private Vector3 origin;

    private Light light;
    private float timer;
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        origin = transform.position;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        light.intensity	= Mathf.Lerp(intensityMin, intensityMax, easeInOutBounce(0.5f + 0.5f * Mathf.Sin(time)));
        transform.position = origin + new Vector3(radius * Mathf.Cos(time) * 0f, Mathf.Sin(time), radius * Mathf.Sin(time) * 0f);
    }

    float easeInOutBounce(float x)
    {
        if (x < 0.5){
            return (1 - easeOutBounce(1 - 2 * x)) / 2;
        }
        else
        {
            return(1 + easeOutBounce(2 * x - 1)) / 2;
        }
    }

    float easeOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1f / d1) {
            return n1 * x * x;
        } else if (x < 2f / d1) {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        } else if (x < 2.5f / d1) {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        } else {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }
}
