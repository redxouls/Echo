using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyLight : MonoBehaviour
{   
    public float radius;
    public float deltaHeight;
    public float intensityMin = 0;
    public float intensityMax = 4;
    public float period = 1;

    private Light light;
    private Vector3 origin, startLocalPosition;
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        origin = transform.position;
        startLocalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Damping();
        
    }

    void Damping()
    {
        float time = Time.time;
        light.intensity	= Mathf.Lerp(intensityMin, intensityMax, EaseInOutBounce(0.5f + 0.5f * Mathf.Sin(time)));
        transform.position = origin + new Vector3(radius * Mathf.Cos(time) * 0f, deltaHeight * Mathf.Sin(time), radius * Mathf.Sin(time) * 0f);
    }

    float EaseInOutBounce(float x)
    {
        if (x < 0.5){
            return (1 - EaseOutBounce(1 - 2 * x)) / 2;
        }
        else
        {
            return(1 + EaseOutBounce(2 * x - 1)) / 2;
        }
    }

    float EaseOutBounce(float x)
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

    public void SetOrigin(Vector3 newOrigin)
    {
        origin = newOrigin + startLocalPosition;
    }
}
