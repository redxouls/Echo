using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveLightManeger : MonoBehaviour
{
    // Set the right postProcessingMaterial
    public Material postProcessingMaterial;

    private int endPassiveIndex;
    private Vector4[] passiveObject;

    // Start is called before the first frame update
    void Start()
    {
        endPassiveIndex = gameObject.transform.childCount;
        passiveObject = new Vector4[endPassiveIndex];
        postProcessingMaterial.SetInt("_EndPassiveIndex", endPassiveIndex);
        for (int i = 0; i < endPassiveIndex; ++i)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            passiveObject[i].x = child.transform.position.x;
            passiveObject[i].y = child.transform.position.y;
            passiveObject[i].z = child.transform.position.z;
            passiveObject[i].w = child.GetComponent<Light>().range;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLight();
        postProcessingMaterial.SetVectorArray("_PassiveObject", passiveObject);
    }
    private float dir = -1f;
    public float speed = 10f;
    void UpdateLight() {
        float lthreshold = 3f;
        float rthreshold = 5f;
        for (int i = 0; i < endPassiveIndex; ++i)
        {
            float ldis = Mathf.Abs(passiveObject[i].w - lthreshold);
            float rdis = Mathf.Abs(passiveObject[i].w - rthreshold);
            passiveObject[i].w += dir * Time.deltaTime * speed * (0.005f +  Mathf.Pow(rdis, 1.5f));
            if (passiveObject[i].w < lthreshold)
                dir = 1;
            if (passiveObject[i].w > rthreshold)
                dir = -1;
        }
    }
}
