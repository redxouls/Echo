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

    public static ChangeScene Instance{get; private set; }

    void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start() {
        if (! created) {
            PlayerPrefs.SetString("sceneStatus", "Main");
            created = true;
        }
    }

    public void MoveToScene(string sceneName) 
    {
        string sceneStatus = PlayerPrefs.GetString("sceneStatus");
        // Debug.Log(sceneStatus);
        if (sceneStatus == "Main") {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            // PlayerPrefs.SetString("sceneStatus", "Game");
        }
        else if (sceneStatus == "Game") {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            // PlayerPrefs.SetString("sceneStatus", "Main");
        }
    }

    private void MoveToUI() 
    {
        string sceneStatus = PlayerPrefs.GetString("sceneStatus");
        // Debug.Log(sceneStatus);
        if (sceneStatus == "Main") {
            SceneManager.LoadScene("UI", LoadSceneMode.Single);
            // PlayerPrefs.SetString("sceneStatus", "Game");
        }
        else if (sceneStatus == "Game") {
            SceneManager.LoadScene("UI", LoadSceneMode.Single);
            // PlayerPrefs.SetString("sceneStatus", "Main");
        }
    }

    private void MoveToLevel1() 
    {
        string sceneStatus = PlayerPrefs.GetString("sceneStatus");
        // Debug.Log(sceneStatus);
        if (sceneStatus == "Main") {
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
            // PlayerPrefs.SetString("sceneStatus", "Game");
        }
        else if (sceneStatus == "Game") {
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
            // PlayerPrefs.SetString("sceneStatus", "Main");
        }
    }

    public void HandleMoveToUI()
    {
        Invoke("MoveToUI", 0.8f);
    }

    public void HandleMoveToLevel1()
    {
        Invoke("MoveToLevel1", 0.8f);
    }
}
