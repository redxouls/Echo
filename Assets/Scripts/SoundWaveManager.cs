using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SoundWaveManager : MonoBehaviour
{   
    // Set the right postProcessingMaterial
    // public Material postProcessingMaterial;
    public int maxNumOfWave = 100;
    public Wave[] waves;

    // Data for shader, which should be the same as data in waves
    public Vector4[] _Points;  // update only when a wave is added. _Points use xyz only, w is useless. 
    // Use Vector4 because SetVectorArray only supports Vector4[]
    float[] _Radius;    // update every Update()
    float[] _thickness; // update only when a wave is added
    float[] _Attributes;  // wave attributes (e.g DEAD, PLAYER...), update every Update()
    float[] _AlphaAttenuation;  // wave alpha attenuation 0 ~ 1, update every Update()
    // Use float for _Attributes because there is not setIntegerArray function -> use SetFloatArray instead

    // Start is called before the first frame update
    void Start()
    {
        waves = new Wave[maxNumOfWave];
        for (int i = 0; i < maxNumOfWave; ++i)
        {
            waves[i] = new Wave();
        }
        _Points = new Vector4[maxNumOfWave];
        _Radius = new float[maxNumOfWave];
        _AlphaAttenuation = new float[maxNumOfWave];
        _thickness = new float[maxNumOfWave];
        _Attributes = new float[maxNumOfWave];
    }

    // Update is called once per frame
    void Update()
    {   
       UpdateWave();
    }

    void UpdateWave() // Update _Radius, _Attributes to shader
    {
        for (int i = 0; i < maxNumOfWave; ++i)
        {
            waves[i].Update();
            _Radius[i] = waves[i].GetRadius();
            _AlphaAttenuation[i] = waves[i].GetAlphaAttenuation();
            _Attributes[i] = (float)waves[i].GetAttribute();
        }
        Shader.SetGlobalFloatArray("_Radius", _Radius);
        Shader.SetGlobalFloatArray("_AlphaAttenuation", _AlphaAttenuation);
        Shader.SetGlobalFloatArray("_Attributes", _Attributes);
    }

    public void AddWave(float thickness, float lifeSpan, float speed, float alphaAttenuation, Vector3 position, WAVE_ATTRIBUTE attribute)
    {
        for (int i = 0; i < maxNumOfWave; ++i)
        {
            if (waves[i].IsDead())
            {
                waves[i].Init(thickness, lifeSpan, speed, alphaAttenuation, position, attribute);
                _Points[i] = position;
                _Radius[i] = 0;
                _AlphaAttenuation[i] = alphaAttenuation;
                _thickness[i] = thickness;
                _Attributes[i] = (float)attribute;
                
                // Update _Points, _Thickness to shader
                Shader.SetGlobalVectorArray("_Points", _Points);
                Shader.SetGlobalFloatArray("_thickness", _thickness);
                return;
            }
        }
        Debug.Log("EEEEEEEorror: wave is out of ragne");
    }

    private IEnumerator AddWaveIEnum(float delay, float thickness, float lifeSpan, float speed, float alphaAttenuation, Vector3 position, WAVE_ATTRIBUTE attribute)
    {
        yield return new WaitForSeconds(delay);
        AddWave(thickness, lifeSpan, speed, alphaAttenuation, position, attribute);
    }

    public void AddWaveSet(float interval, int count, float thickness, float lifeSpan, float speed, float alphaAttenuation, Vector3 position, WAVE_ATTRIBUTE attribute)
    {
        for (int i = 0; i < count; ++i)
        {
            StartCoroutine(AddWaveIEnum(i*interval, thickness, lifeSpan, speed, alphaAttenuation, position, attribute));
        }
    }
}


