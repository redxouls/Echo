using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleatAllData : MonoBehaviour
{
    void OnApplicationQuit() {
        Debug.Log("Application ending after " + Time.time + " seconds");
        PlayerPrefs.DeleteAll();
    }
}
