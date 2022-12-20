using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLightManager : MonoBehaviour
{
    // static members for TriggerLight, set by the values in inspector
    public float raisingTime;
    public float maxRange;
    public float stayLightInterval;
    public float triggerThickness;


    
    Vector4[] _TriggerLight;
    float[] _TriggerLightRadius;
    int _NofTirggerLight;

    // Start is called before the first frame update
    void Start()
    {
        TriggerLight.raisingTime = raisingTime;
        TriggerLight.maxRange = maxRange;
        TriggerLight.stayLightInterval = stayLightInterval;
        

        Shader.SetGlobalInt("_NofTirggerLight", gameObject.transform.childCount);
        Vector4[] _TriggerLight = new Vector4[100];
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            _TriggerLight[i] = child.transform.position;
            child.gameObject.GetComponent<TriggerLight>().setId(i);
        }
        Shader.SetGlobalVectorArray("_TriggerLight", _TriggerLight);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWaveInformation();
        TriggerLight.triggerThickness = triggerThickness; //
    }

    void UpdateWaveInformation()
    {
        // Update belows static members in TriggerLight: 
        // _Points, _Attributes, _Radius;
        TriggerLight._Points = Shader.GetGlobalVectorArray("_Points");
        TriggerLight._Attributes = Shader.GetGlobalFloatArray("_Attributes");
        TriggerLight._Radius = Shader.GetGlobalFloatArray("_Radius");

        // Update lights' radius from TriggerLight
        Shader.SetGlobalFloatArray("_TriggerLightRadius", TriggerLight._TriggerLightRadius);
    }
}
