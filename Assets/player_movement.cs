using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player_movement : MonoBehaviour
{   
    public float step_size = 1.0f;
    public GameObject prefab;
    public GameObject head;
    private float echo_lifeSpan = 5.0f;
    private float timer = 0.2f;
    private float min_echo_interval = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (timer < min_echo_interval) {
            timer += Time.deltaTime;
        }
        if (Input.GetKey("w")) {
            head.transform.position += new Vector3(-step_size, 0, 0) * Time.deltaTime;
            this.transform.localPosition += new Vector3(-step_size, 0, 0) * Time.deltaTime;
            if (timer >= min_echo_interval) {
                Echo();
                timer = 0.0f;
            }
        }
        if (Input.GetKey("a")) {
            head.transform.position += new Vector3(0, 0, -step_size) * Time.deltaTime;
            this.transform.localPosition += new Vector3(0, 0, -step_size) * Time.deltaTime;
            if (timer >= min_echo_interval) {
                Echo();
                timer = 0.0f;
            }
        }
        if (Input.GetKey("s")) {
            head.transform.position += new Vector3(step_size, 0, 0) * Time.deltaTime;
            this.transform.localPosition += new Vector3(step_size, 0, 0) * Time.deltaTime;
            if (timer >= min_echo_interval) {
                Echo();
                timer = 0.0f;
            }
        }
        if (Input.GetKey("d")) {
            head.transform.position += new Vector3(0, 0, step_size) * Time.deltaTime;
            this.transform.localPosition += new Vector3(0, 0, step_size) * Time.deltaTime;
            if (timer >= min_echo_interval) {
                Echo();
                timer = 0.0f;
            }
        }
    }

    void Echo() {
        GameObject echo = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        echo.SetActive(true);
        Destroy(echo, echo_lifeSpan);
    }
}
