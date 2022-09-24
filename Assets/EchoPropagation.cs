using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoPropagation : MonoBehaviour
{   
    public float speed = 20.0f;
    public Transform player;

    private Vector3 scale = new Vector3(1, 1, 1);
    private float timer = 0.0f;
    private float strength = 0.0f;


    // Start is called before the first frame update
    void Start()
    {   
        transform.localPosition = player.transform.localPosition;
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {   
        timer += Time.deltaTime * speed;
        transform.localScale = scale * (1 + timer);
        strength =  0.25f * Mathf.Pow(1 + timer, -1.5f);
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Transparency", strength);
    }
}
