using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoPropagation : MonoBehaviour
{   
    private Vector3 scale = new Vector3(1, 1, 1);
    private float timer = 0.0f;
    public float speed = 20.0f;
    public Transform player;

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
    }
}
