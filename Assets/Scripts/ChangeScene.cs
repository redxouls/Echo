using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{   
    public Canvas InitialCanvas;
    public Canvas MainCanvas;
    public Canvas SettingCanvas;

    private static bool created = false;

    void Start() {
        if (! created) {
            PlayerPrefs.SetString("sceneStatus", "Main");
            created = true;
        }
    }

    public void MoveToScene(string sceneName) {
        string sceneStatus = PlayerPrefs.GetString("sceneStatus");
        // Debug.Log(sceneStatus);
        if (sceneStatus == "Main") {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            PlayerPrefs.SetString("sceneStatus", "Game");
        }
        else if (sceneStatus == "Game") {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            PlayerPrefs.SetString("sceneStatus", "Main");
        }
    }

    void Update() {
        
    }

    // GameObject FindInactiveObjectsByName(string objectName) {
    //     GameObject[] objects = GameObject.FindObjectsOfType<GameObject>(true);
    //     for(int i = 0; i < objects.Length; i++) {
    //         if (objects[i].name == objectName) {
    //             return objects[i];
    //         }
    //     }
    //     return null;
    // }
}
