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
    public int maxNumOfWave = 100; // TODO: this should be const to avoid 
    public Vector4[] points;
    public enum WAVE_ATTRIBUTE {
        PLAYER,
        ENV,
        PROP
    }
    public WAVE_ATTRIBUTE[] waveAttributes;
    // public Vector4[] settings;
    public int startIndex;
    public int endIndex;

    // Start is called before the first frame update
    void Start()
    {
        startIndex = 0;
        endIndex = 0;
        points = new Vector4[maxNumOfWave];
        waveAttributes = new WAVE_ATTRIBUTE[maxNumOfWave];
        postProcessingMaterial.SetFloat("_Thickness", thickness);
    }

    // Update is called once per frame
    void Update()
    {   
        UpdateWave();
    }

    public void UpdateWave()
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
        startIndex = newStartIndex % points.Length;
        postProcessingMaterial.SetInt("_StartIndex", startIndex);
        postProcessingMaterial.SetInt("_EndIndex", endIndex);
        postProcessingMaterial.SetVectorArray("_Points", points);
    }

    public void AddWave(Vector3 waveSourcePosition, WAVE_ATTRIBUTE waveAttribute)
    {
        points[endIndex] = new Vector4(waveSourcePosition.x, waveSourcePosition.y, waveSourcePosition.z, 0);
        waveAttributes[endIndex] = waveAttribute;
        endIndex = (endIndex + 1) % points.Length;
    }

    private IEnumerator AddWaveIEnum(Vector3 waveSourcePosition, float delay, WAVE_ATTRIBUTE waveAttribute)
    {
        yield return new WaitForSeconds(delay);
        AddWave(waveSourcePosition, waveAttribute);
    }

    public void AddWaveSet(Vector3 waveSourcePosition, float interval, int count, WAVE_ATTRIBUTE waveAttribute)
    {
        for (int i = 0; i < count; ++i) {
            StartCoroutine(AddWaveIEnum(waveSourcePosition, i*interval, waveAttribute));
        }
    }
}
