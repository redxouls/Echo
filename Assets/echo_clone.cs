using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class echo_clone : MonoBehaviour
{
    public GameObject prefab;
    private float lifeSpan = 5.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown("space")) {
            Debug.Log("echo");
            GameObject echo = Instantiate(prefab, gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(echo, lifeSpan);
        }
    }
}
