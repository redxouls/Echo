using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CollectGem : MonoBehaviour
{
    bool collected;
    public float fadeoutFactor;
    float timer;
    public Material gemMaterial;
    public GameObject Collect; // TODO: add particle effect
    public CollectionManager collectionMgr;
    public string gemName;
    
    AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gemMaterial.SetFloat("_Transparency", 1);
        // gemMaterial.SetFloat("_FresnelPower", 0.2f);
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        // gemMaterial.SetFloat("_Transparency", 0);
        if (collected)
        {
            // Collect.Play();
            Collect.SetActive(true);
            BeingCollected();
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !collected)
        {
            collected = true;
            timer = 0;
            audioSource.PlayOneShot(clip, 0.7f);
        }
    }
    void BeingCollected()
    {
        timer += Time.deltaTime;
        float factor = Mathf.Pow(fadeoutFactor, timer);
        float factorFresnel = 0.2f + timer*0.1f;
        // gemMaterial.SetFloat("_FresnelPower", factorFresnel);
        gemMaterial.SetFloat("_Transparency", factor);
        if (factor < 0.001)
        {
            gemMaterial.SetFloat("_Transparency", 0);
            collectionMgr.CollectGem(gemName);
        }
    }
}
