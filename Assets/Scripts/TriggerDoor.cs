using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public GameObject RedGem;
    public GameObject BlueGem;
    public GameObject GreenGem;
    public GameObject YellowGem;

    public Material GemMaterial;
    public CollectionManager collectionMgr;
    public float fadeinFactor;
    float timer = 0f;
    float factor = 0f;
    bool triggered;
    void Start()
    {
        triggered = false;
        GemMaterial.SetFloat("_Transparency", 0);
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Player" && !triggered)
        {
            collectionMgr.CompletetGem("Fire");
            Debug.Log("TRIGGGGGGER");
            triggered = true;
            timer = 0f;
        }
        Debug.Log(collisionInfo.tag);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered && factor <= 0.9f){
            timer += Time.deltaTime * 0.1f;
            factor = 1f - Mathf.Pow(fadeinFactor, timer);
            Debug.Log(factor);
            GemMaterial.SetFloat("_Transparency", factor);
        }
        if (factor > 0.9f)
        {
            GemMaterial.SetFloat("_Transparency", 1);
        }
    }
}
