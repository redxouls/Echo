using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public GameObject RedGem;
    public GameObject BlueGem;
    public GameObject GreenGem;
    public GameObject YellowGem;

    public Material[] RBGY_mat; // length = 4, order should be R, B, G, Y

    float[] timer;
    bool[] completed;

    public CollectionManager collectionMgr;
    public float fadeinFactor;
    
    void Start()
    {
        timer = new float[4]{0, 0, 0, 0};
        completed = new bool[4]{false, false, false, false};
        for (int i = 0; i < 4; ++i)
        {
            SetTransparency(i, 0);
        }
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Player")
        {
            CompletetGem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowGem();
    }

    void CompletetGem()
    {
        string[] gemName = new string[4]{"Fire", "Water", "Grass", "Light"};
        int gemCollection = collectionMgr.GetGemColletion();
        for (int i = 0; i < 4; ++i)
        {
            if ((gemCollection & (1 << i)) > 0)
            {
                collectionMgr.CompletetGem(gemName[i]);
                timer[i] = 0;
                completed[i] = true;
            }
        }
    }

    void ShowGem()
    {
        for (int i = 0; i < 4; ++i)
        {
            if (completed[i])
            {
                timer[i] += Time.deltaTime * 0.01f;
                float factor = 1f - Mathf.Pow(fadeinFactor, timer[i]);
                SetTransparency(i, factor);
                // RBGY_mat[i].SetFloat("_Transparency", factor);
            }
        }
    }

    void SetTransparency(int index, float value)
    {
        Color color = RBGY_mat[index].color;
        color.a = value;
        RBGY_mat[index].color = color;
    }
}
