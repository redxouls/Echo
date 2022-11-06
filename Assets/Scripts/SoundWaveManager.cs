using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveManager : MonoBehaviour
{   
    #region Struction
    [System.Serializable]
    public struct WaveData
    {
        public float speed;
        public float lifeSpan;
        public float thickness;
        public Vector4[] waves;
        public int maxWaveNum;
        public int waveNum;
        public string shaderArr;
        public string shaderNum;
    }
    #endregion
    public WaveData playerWave, grenadeWave, envLight, passiveLightWave;
    int dir = 1;
    float speed = 10f;
    // Set the right postProcessingMaterial
    public Material postProcessingMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerWaveSetup();
        GrenadeWaveSetup();
        EnvLightSetup();
        PassiveLightWaveSetup();
    }

    // Update is called once per frame
    void Update()
    {   
        UpdateWave(ref playerWave);
        UpdateWave(ref grenadeWave);
        // UpdateWave(ref passiveLightWave);
        UpdateEnvLight();
    }

    void UpdateWave(ref WaveData wave)
    {
        int newNum = wave.waveNum;
        for (int i = 0; i < wave.waveNum; ++i) 
        {
            wave.waves[i].w += Time.deltaTime * wave.speed;
            if (wave.waves[i].w > wave.lifeSpan * wave.speed)
            {
                newNum--;
            }
        }
        for (int i = 0; i < newNum; ++i) {
            wave.waves[i] = wave.waves[i + wave.waveNum - newNum];
        }
        wave.waveNum = newNum;
        postProcessingMaterial.SetInt(wave.shaderNum, wave.waveNum);
        postProcessingMaterial.SetVectorArray(wave.shaderArr, wave.waves);
    }
    void UpdateEnvLight()
    {
        float lthreshold = 3f;
        float rthreshold = 5f;
        for (int i = 0; i < envLight.waveNum; ++i)
        {
            float ldis = Mathf.Abs(envLight.waves[i].w - lthreshold);
            float rdis = Mathf.Abs(envLight.waves[i].w - rthreshold);
            envLight.waves[i].w += dir * Time.deltaTime * speed * (0.005f +  Mathf.Pow(rdis, 1.5f));
            if (envLight.waves[i].w < lthreshold)
                dir = 1;
            if (envLight.waves[i].w > rthreshold)
                dir = -1;
        }
        postProcessingMaterial.SetVectorArray(envLight.shaderArr, envLight.waves);
    }

    /*
    public void AddWave()//Vector3 waveSourcePosition, WAVE_ATTRIBUTE waveAttribute)
    {
        points[endIndex] = new Vector4(waveSourcePosition.x, waveSourcePosition.y, waveSourcePosition.z, 0);
        waveAttributes[endIndex] = waveAttribute;
        endIndex = (endIndex + 1) % points.Length;
    }

    private IEnumerator AddWaveIEnum()//Vector3 waveSourcePosition, float delay, WAVE_ATTRIBUTE waveAttribute)
    {
        yield return new WaitForSeconds(delay);
        AddWave(waveSourcePosition, waveAttribute);
    }

    public void AddWaveSet()//Vector3 waveSourcePosition, float interval, int count, WAVE_ATTRIBUTE waveAttribute)
    {
        for (int i = 0; i < count; ++i) {
            StartCoroutine(AddWaveIEnum(waveSourcePosition, i*interval, waveAttribute));
        }
    }
    */

    void PlayerWaveSetup()
    {
        playerWave.maxWaveNum = 10;
        playerWave.waves = new Vector4[playerWave.maxWaveNum];
        playerWave.waveNum = 0;
        playerWave.shaderArr = "_PlayerWaves";
        playerWave.shaderNum = "_PlayerWaveNum";

        postProcessingMaterial.SetFloat("_PlayerWaveThickness", playerWave.thickness);
        postProcessingMaterial.SetVectorArray(playerWave.shaderArr, playerWave.waves);
        postProcessingMaterial.SetInt("_PlayerWaveNum", playerWave.waveNum);
    }
    void GrenadeWaveSetup()
    {
        grenadeWave.maxWaveNum = 10;
        grenadeWave.waves = new Vector4[grenadeWave.maxWaveNum];
        grenadeWave.waveNum = 0;
        grenadeWave.shaderArr = "_GrenadeWaves";
        grenadeWave.shaderNum = "_GrenadeWaveNum";

        postProcessingMaterial.SetFloat("_GrenadeWaveThickness", grenadeWave.thickness);
        postProcessingMaterial.SetVectorArray(grenadeWave.shaderArr, grenadeWave.waves);
        postProcessingMaterial.SetInt("_GrenadeWaveNum", grenadeWave.waveNum);
    } 
    void EnvLightSetup()
    {
        envLight.maxWaveNum = 100;
        envLight.waves = new Vector4[envLight.maxWaveNum];
        GameObject envLightParent = GameObject.Find("EnvLight");
        envLight.waveNum = envLightParent.transform.childCount;
        envLight.shaderArr = "_EnvLights";
        // the number of envLight should not change, so don't need to set num
        for (int i = 0; i < envLight.waveNum; ++i)
        {
            GameObject child = envLightParent.transform.GetChild(i).gameObject;
            envLight.waves[i].x = child.transform.position.x;
            envLight.waves[i].y = child.transform.position.y;
            envLight.waves[i].z = child.transform.position.z;
            envLight.waves[i].w = child.GetComponent<Light>().range;
        }
        postProcessingMaterial.SetVectorArray(envLight.shaderArr, envLight.waves);
        postProcessingMaterial.SetInt("_EnvLightNum", envLight.waveNum);
    }
    void PassiveLightWaveSetup()
    {

    }

    void AddWave(ref WaveData wave, Vector3 position)
    {
        wave.waves[wave.waveNum].x = position.x;
        wave.waves[wave.waveNum].y = position.y;
        wave.waves[wave.waveNum].z = position.z;
        wave.waves[wave.waveNum].w = 0;
        wave.waveNum++;
    }
    public void AddGrenadeWave(Vector3 position)
    {
        AddWave(ref grenadeWave, position);
    }
    public void AddPlayerWave(Vector3 position)
    {
        AddWave(ref playerWave, position);
    }
    public void AddPassiveLightWave(Vector3 position)
    {
        AddWave(ref passiveLightWave, position);
    }
}
