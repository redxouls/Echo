using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySetting : MonoBehaviour
{
    private static DontDestroySetting Instance;

    void Awake() {
        // DontDestroyOnLoad(this.gameObject);
        // if (Instance == null) {
        //     Instance = this;
        // } else {
        //     Destroy(gameObject);
        // }
    }
}
