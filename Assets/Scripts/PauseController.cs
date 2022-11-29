using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool GamePaused;
    public PlayerMovement player;
    private float triggerTime = 1f;
    private float timer;

    public GameObject PauseCanvas;

    void Start()
    {
        GamePaused = false;
        timer = triggerTime;
        PauseCanvas.SetActive(GamePaused);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !player.isDead)
        {   
            ChangePauseState();        
        }
    }

    void ChangePauseState()
    {
        GamePaused = !GamePaused;
        // Debug.Log("GamePaused: " + GamePaused);
        Time.timeScale = GamePaused ? 0f : 1f;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        if (GamePaused)
        {
            foreach (AudioSource audio in audios)
            {
                audio.Pause();
            }
            PauseCanvas.SetActive(GamePaused);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            // Screen.lockCursor = false;
        }
        else
        {
            foreach (AudioSource audio in audios)
            {
                audio.Play();
            }
            PauseCanvas.SetActive(GamePaused);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            // Screen.lockCursor = true;
        }
    }

    public void ResumeButton()
    {
        ChangePauseState();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Single);
        ChangePauseState();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        ChangePauseState();
    }
}
