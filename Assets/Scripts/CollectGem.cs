using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGem : MonoBehaviour
{
    bool collected;
    public float fadeoutFactor;
    float timer;
    public Material gemMaterial;
    public GameObject LevelUp; // TODO: add particle effect
    public CollectionManager collectionMgr;
    public string gemName;
    // Start is called before the first frame update
    void Start()
    {
        gemMaterial.SetFloat("_Transparency", 1);
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        // gemMaterial.SetFloat("_Transparency", 0);
        if (collected)
        {
            BeingCollected();
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !collected)
        {
            collected = true;
            timer = 0;
        }
    }
    void BeingCollected()
    {
        timer += Time.deltaTime;
        float factor = Mathf.Pow(fadeoutFactor, timer);
        gemMaterial.SetFloat("_Transparency", factor);
        if (factor < 0.01)
        {
            gemMaterial.SetFloat("_Transparency", 0);
            // LevelUp.SetActive(false);
            collectionMgr.CollectGem(gemName);
        }
    }
}
