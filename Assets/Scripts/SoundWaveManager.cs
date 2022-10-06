using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveManager : MonoBehaviour
{   
    // Settings for wave
    [Range(0.0f, 20.0f)]
    public float waveSpeed = 5.0f;
    [Range(0.0f, 20.0f)]
    public float echoLifeSpan = 5.0f;
    [Range(0.0f, 1.0f)]
    public float thickness = 0.3f;
    
    // Set the right postProcessingMaterial
    public Material postProcessingMaterial;

    // Internal sound wave data structure. Use array as a Queue
    public Vector4[] points;
    public Vector4[] settings;
    public int startIndex;
    public int endIndex;

    // Start is called before the first frame update
    void Start()
    {
        startIndex = 0;
        endIndex = 0;
        points = new Vector4[100];
        postProcessingMaterial.SetFloat("_Thickness", thickness);
    }

    // Update is called once per frame
    void Update()
    {   
        UpdateWaveSource();
    }

    public void UpdateWaveSource()
    {
        int newStartIndex = startIndex;
        for (int i = 0; i < (endIndex + points.Length - startIndex) % points.Length; i++) 
        {
            int index = (startIndex + i) % points.Length;
            points[index].w += Time.deltaTime * waveSpeed;
            if (points[index].w > echoLifeSpan * waveSpeed)
            {
                newStartIndex ++;
            }
        }
        startIndex = newStartIndex;
        postProcessingMaterial.SetInt("_StartIndex", startIndex);
        postProcessingMaterial.SetInt("_EndIndex", endIndex);
        postProcessingMaterial.SetVectorArray("_Points", points);
    }

    public void AddWaveSource(Vector4 waveSourcePosition)
    {
        points[endIndex] = new Vector4(waveSourcePosition.x, waveSourcePosition.y, waveSourcePosition.z, 0);
        endIndex = (endIndex + 1) % points.Length;
    }

}
