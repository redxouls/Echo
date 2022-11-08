using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvLightManager : MonoBehaviour
{
    // Set the right postProcessingMaterial
    public Material postProcessingMaterial;

    int maxNumOfWave = 100;
    float[] largestRange;
    float[] smallestRange;
    float[] envLightSpeed;
    int [] envLightDir;

    int _EnvLightNum;
    Vector4[] _EnvLightPoints;
    float[] _EnvLightRadius;

    // Start is called before the first frame update
    void Start()
    {
        largestRange = new float[maxNumOfWave];
        smallestRange = new float[maxNumOfWave];
        envLightSpeed = new float[maxNumOfWave];
        envLightDir = new int[maxNumOfWave];
        _EnvLightPoints = new Vector4[maxNumOfWave];
        _EnvLightRadius = new float[maxNumOfWave];

        _EnvLightNum = gameObject.transform.childCount;
        for (int i = 0; i < _EnvLightNum; ++i)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            _EnvLightPoints[i] = child.transform.position;
            _EnvLightRadius[i] = child.GetComponent<Light>().range;
            largestRange[i] = child.GetComponent<Light>().range;
            smallestRange[i] = Random.Range(0.5f * largestRange[i], 0.75f * largestRange[i]);
            envLightSpeed[i] = Random.Range( largestRange[i], 2 * largestRange[i]);
            envLightDir[i] = 2 * Random.Range(0, 2) - 1; // randomly set to 1 or -1
        }
        postProcessingMaterial.SetVectorArray("_EnvLightPoints", _EnvLightPoints);
        postProcessingMaterial.SetInt("_EnvLightNum", _EnvLightNum);
        postProcessingMaterial.SetFloatArray("_EnvLightRadius", _EnvLightRadius);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _EnvLightNum; ++i)
        {
            float smallestDis = Mathf.Abs(_EnvLightRadius[i] - smallestRange[i]);
            float largestDis = Mathf.Abs(_EnvLightRadius[i] - largestRange[i]);
            _EnvLightRadius[i] += envLightDir[i] * Time.deltaTime * envLightSpeed[i] * (0.005f +  Mathf.Pow(largestDis, 1.5f));
            if (_EnvLightRadius[i] < smallestRange[i])
                envLightDir[i] = 1;
            if (_EnvLightRadius[i] > largestRange[i])
                envLightDir[i] = -1;
        }
        postProcessingMaterial.SetFloatArray("_EnvLightRadius", _EnvLightRadius);
    }
}
