using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitSetting : MonoBehaviour
{

    public Slider waveSpeed;
    public Slider waveLifespan;
    public Slider minEchoInterval;
    public Slider playerSpeed;
    public Slider Volume;
    public Slider waveThickness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        Debug.Log("INITTTT");
        waveSpeed.value = 3;
        waveLifespan.value = 5;
        minEchoInterval.value = 2f;
        playerSpeed.value = 1.5f;
        Volume.value = 5;
        waveThickness.value = 2;
    }
}
