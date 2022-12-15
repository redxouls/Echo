using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool GamePaused;
    public static bool inTutorPage;
    public PlayerMovement player;
    private float triggerTime = 1f;
    private float timer;

    public GameObject PauseCanvas;
    public GameObject TutorialCanvas;

    void Start()
    {
        GamePaused = false;
        inTutorPage = false;
        timer = triggerTime;
        PauseCanvas.SetActive(GamePaused);
        TutorialCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !player.isDead && !inTutorPage)
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
                audio.UnPause();
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

    public void MainMenuButtonDelay()
    {
        // Invoke("MainMenuButton", 1.0f);
        ChangeScene.Instance.HandleMoveToUI();
        if (!player.isDead)
        {
            ChangePauseState();
        }
    }

    public void RestartButtonDelay()
    {
        // Invoke("RestartButton", 1.0f);
        ChangeScene.Instance.HandleMoveToLevel1();
        if (!player.isDead)
        {
            ChangePauseState();
        }
    }

    public void ActivateTutorialCanvas()
    {
        TutorialCanvas.SetActive(true);
        inTutorPage = true;
    }

    public void InactivateTutorialCanvas()
    {
        TutorialCanvas.SetActive(false);
        inTutorPage = false;
    }
}
