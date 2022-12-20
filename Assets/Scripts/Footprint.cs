using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [Tooltip("this is how long the decal will stay, before it shrinks away totally")]
    public float Lifetime = 2.0f;

    private float mark;
    private Vector3 OrigSize;
    private Material foot;
    private Color foot_color;
    // Start is called before the first frame update
    void Start()
    {
        mark = Time.time;
        OrigSize = this.transform.localScale;
        foot = this.GetComponent<Renderer>().material;
        foot_color = foot.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        float ElapsedTime = Time.time - mark;
        if (ElapsedTime != 0)
        {
            float PercentTimeLeft = (Lifetime - ElapsedTime) / Lifetime;

            // this.transform.localScale = new Vector3(OrigSize.x * PercentTimeLeft, OrigSize.y * PercentTimeLeft, OrigSize.z * PercentTimeLeft);

            // var color = this.GetComponent<Renderer>().material.color;
            // color.a = PercentTimeLeft;
            // this.GetComponent<Renderer>().material.color = color;
            Material foot = this.GetComponent<Renderer>().material;
            foot.SetColor("_EmissionColor", foot_color * PercentTimeLeft);
            // Debug.Log(foot_color * PercentTimeLeft);
            if (ElapsedTime > Lifetime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
