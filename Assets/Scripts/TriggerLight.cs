using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLight : MonoBehaviour
{
    Light pointLight;
    bool triggered;
    float timer;

    static public float raisingTime;
    static public float maxRange;
    static public float stayLightInterval;

    
    static public float[] _TriggerLightRadius = new float[100];
    int id; // each light with different id (assigned start from 0), and set position and radius accordingly

    static int numberOfPoints = 100;
    static public Vector4[] _Points;
    static public float[] _Radius;
    static public float[] _Attributes;
    static public float triggerThickness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = gameObject.transform.GetChild(0).GetComponent<Light>();
        pointLight.range = 0;
        _Points = new Vector4[numberOfPoints];
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        TriggeredByWave();
        HandleLight();
    }
    
    public void setId(int num) {id = num;}

    // Triggered if pass by a wave front
    void TriggeredByWave()
    {
        if (triggered)
            return;
        
        for (int i = 0; i < numberOfPoints; ++i)
        {
            if (_Attributes[i] == 0)
            {
                continue;
            }
            float distance = Vector3.Distance(_Points[i], transform.position);
            Debug.LogFormat("id: {0}, distance: {1}, radius: {2}", id, distance, _Radius[i]);
            if (_Radius[i] < distance && distance < _Radius[i] + triggerThickness)
            {
                // Debug.Log(id);
                triggered = true;
                timer = 0f;
            }
        }
    }

    void HandleLight()
    {
        if (!triggered)
        {
            pointLight.range = 0;
            _TriggerLightRadius[id] = 0;
            return;
        }
        timer += Time.deltaTime;
        float range = 0f;
        if (timer <= raisingTime)
        {
            range = maxRange * easeOutElastic(timer / raisingTime);
        }
        if (timer > raisingTime)
        {
            range = maxRange;
        }
        if (timer > stayLightInterval)
        {
            range = maxRange * easeOutExpo(1 - (timer-stayLightInterval) / raisingTime);
        }
        if (timer > stayLightInterval + raisingTime)
        {
            range = 0f;
            triggered = false;
        }
        pointLight.range = range;
        _TriggerLightRadius[id] = range;
    }

    float easeOutExpo(float t)
    {
        return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
    }

    float easeOutElastic(float t)
    {
        float c4 = (2 * Mathf.PI) / 3;
        if (t <= 0)
            return 0;
        if (t >= 1) 
            return 1;
        return Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * c4) + 1;
    }
}
