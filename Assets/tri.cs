using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tri : MonoBehaviour
{
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Player")
        {
            light.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Player")
        {
            light.SetActive(true);
        }
    }
}
