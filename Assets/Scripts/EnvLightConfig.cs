using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvLightConfig : MonoBehaviour
{
    public float smallestRange;
    public float largestRange;
    public float speed;
    public int direction;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 1) > 0.5 )
        {
            direction = 1;
        }
        else 
        {
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
