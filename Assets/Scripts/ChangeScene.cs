using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{   
    public GameObject InitialCanvas;
    public GameObject MainCanvas;
    public GameObject SettingCanvas;

    void Start() {
        if (PlayerPrefs.GetString("sceneStatus") == null) {
            PlayerPrefs.SetString("sceneStatus", "Main");
        }
    }

    public void MoveToScene(string sceneName) {
        string sceneStatus = PlayerPrefs.GetString("sceneStatus");
        Debug.Log(sceneStatus);
        if (sceneStatus == "Main") {
            SceneManager.LoadScene(sceneName);
            Debug.Log(sceneName);
            InitialCanvas.SetActive(false);
            MainCanvas.SetActive(false);
            SettingCanvas.SetActive(false);
            PlayerPrefs.SetString("sceneStatus", "Game");
        }
        else if (sceneStatus == "Game") {
            Debug.Log(sceneStatus);
            SceneManager.LoadScene(sceneName);
            Debug.Log(sceneName);
            MainCanvas = FindInactiveObjectsByName("MainCanvas");
            MainCanvas.SetActive(true);
            PlayerPrefs.SetString("sceneStatus", "Main");
        }
    }

    void Update() {
        string sceneStatus = PlayerPrefs.GetString("sceneStatus");
        if (Input.GetKey("q")){
            Debug.Log("press q");
            Debug.Log(sceneStatus);
            if (sceneStatus == "Game") {
                MoveToScene("UI");
            }
        }
    }

    GameObject FindInactiveObjectsByName(string objectName) {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>(true);
        for(int i = 0; i < objects.Length; i++) {
            if (objects[i].name == objectName) {
                return objects[i];
            }
        }
        return null;
    }
}
