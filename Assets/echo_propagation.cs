using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class echo_propagation : MonoBehaviour
{   
    private Vector3 scale = new Vector3(1, 1, 1);
    private float timer = 0.0f;
    public float speed = 10.0f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {   
        this.transform.localPosition = player.transform.localPosition;
        this.gameObject.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {   
        timer += Time.deltaTime * speed;
        this.gameObject.transform.localScale = scale * (1 + timer);
        // Debug.Log(1 + timer);
        // if (1 + timer >= 20) {timer = 0;}
    }
}
