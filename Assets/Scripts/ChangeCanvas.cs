using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCanvas : MonoBehaviour
{
    public GameObject InitialCanvas;
    public GameObject MainCanvas;
    public GameObject SettingCanvas;

    // Start is called before the first frame update
    void Start(){
        InitialCanvas.SetActive(true);
        MainCanvas.SetActive(false);
        SettingCanvas.SetActive(false);
    }

    public void SetCanvasActive(string canvasName) {
        if (canvasName == "InitialCanvas") {
            InitialCanvas.SetActive(true);
            MainCanvas.SetActive(false);
            SettingCanvas.SetActive(false);
        }   
        else if (canvasName == "MainCanvas") {
            InitialCanvas.SetActive(false);
            MainCanvas.SetActive(true);
            SettingCanvas.SetActive(false);
        }
        else if (canvasName == "SettingCanvas") {
            InitialCanvas.SetActive(false);
            MainCanvas.SetActive(false);
            SettingCanvas.SetActive(true);
        }
    }
}
