using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool GamePaused;
    private float triggerTime = 1f;
    private float timer;

    void Start()
    {
        GamePaused = false;
        timer = triggerTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            Debug.Log("Escape");
            GamePaused = !GamePaused;
            ChangeGameState();        
        }
    }

    void ChangeGameState()
    {
        Time.timeScale = GamePaused ? 0f : 1f;
    }
}
