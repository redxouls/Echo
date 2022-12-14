using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCanvas : MonoBehaviour
{
    public Canvas InitialCanvas;
    public Canvas MainCanvas;
    public Canvas SettingCanvas;
    public Canvas TutorialCanvas;

    private static bool created = false;
    void Start(){
        if (InitialCanvas)
        {
            if (!created) {
                SetCanvasActive("InitialCanvas");
                created = true;
            }
            else {
                SetCanvasActive("MainCanvas");
            }
        }
    }

    public void SetCanvasActive(string canvasName) {
        if (canvasName == "InitialCanvas") {
            InitialCanvas.enabled = true;
            MainCanvas.enabled = false;
            SettingCanvas.enabled = false;
            TutorialCanvas.enabled = false;
        }   
        else if (canvasName == "MainCanvas") {
            InitialCanvas.enabled = false;
            MainCanvas.enabled = true;
            SettingCanvas.enabled = false;
            TutorialCanvas.enabled = false;
        }
        else if (canvasName == "SettingCanvas") {
            InitialCanvas.enabled = false;
            MainCanvas.enabled = false;
            SettingCanvas.enabled = true;
            TutorialCanvas.enabled = false;
        }
        else if (canvasName == "TutorialCanvas")
        {
            InitialCanvas.enabled = false;
            MainCanvas.enabled = false;
            SettingCanvas.enabled = false;
            TutorialCanvas.enabled = true;
        }
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
