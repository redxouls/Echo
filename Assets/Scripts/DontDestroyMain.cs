using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMain : MonoBehaviour
{
    private static DontDestroyMain Instance;

    void Awake() {
        // DontDestroyOnLoad(this.gameObject);
        // if (Instance == null) {
        //     Instance = this;
        // } else {
        //     Destroy(gameObject);
        // }
    }
}
