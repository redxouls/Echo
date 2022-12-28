using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ActivateGem : MonoBehaviour
{
    public bool triggered;
    public bool direction; // 0:collect 1:complete
    public float transparency;
    public float fadeFactor;
    public float timer;
    public Material gemMaterial;
    public GameObject Effect;
    public CollectionManager collectionMgr;
    public string gemName;
    AudioSource audioSource;
    public AudioClip clip;

    private bool effectStarted;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        triggered = false;
        effectStarted = false;
        if (direction)
        {
            gemMaterial.SetFloat("_Transparency", transparency);
        }
        else
        {
            gemMaterial.SetFloat("_Transparency", transparency);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (triggered & !direction)
        {
            Deactivate();
        }
        else if (triggered & direction)
        {
            Activate();
        }
        else
        {
            effectStarted = false;
        }
    }

    void Deactivate()
    {
        timer += Time.deltaTime;
        float factor = Mathf.Pow(fadeFactor, timer);

        if (!effectStarted)
        {
            audioSource.PlayOneShot(clip, 0.7f);
            Effect.SetActive(true);
            effectStarted = true;
        }

        gemMaterial.SetFloat("_Transparency", factor);
        if (factor < 0.001)
        {
            gemMaterial.SetFloat("_Transparency", 0);
            collectionMgr.CollectGem(gemName);
            triggered = false;
            gameObject.SetActive(false);
        }
    }
    
    void Activate()
    {
        timer += Time.deltaTime;
        float factor = 1f - Mathf.Pow(fadeFactor, timer);

        if (!effectStarted)
        {
            audioSource.PlayOneShot(clip, 0.7f);
            Effect.SetActive(true);
            effectStarted = true;
        }

        gemMaterial.SetFloat("_Transparency", factor);
        if (factor > 0.9)
        {
            gemMaterial.SetFloat("_Transparency", 1);
            collectionMgr.CollectGem(gemName);
            triggered = false;
        }
    }
}
