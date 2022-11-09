using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBreath : MonoBehaviour
{
    int dir;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        dir = 2 * Random.Range(0, 2) - 1; // dir = 1 / -1
        speed = Random.Range(0.5f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        var color = gameObject.GetComponent<Image>().color;
        if (color.a > 0.99f)
        {
            dir = -1;
        }
        if (color.a < 0.4f)
        {
            dir = 1;
        }
        color.a += dir * speed * Time.deltaTime;
        gameObject.GetComponent<Image>().color = color;
    }
}
