using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public CollectionManager collectionManager;
    public GameObject EndCanvas;
    public GameObject PlayerStatusCanvas;
    public VideoPlayer videoPlayer;
    public Canvas TransitionCanvas;
    public GameObject TransitionMask;
    private bool fadeActive;
    private bool win;
    int dir;
    // Start is called before the first frame update
    void Start()
    {
        EndCanvas.SetActive(false);
        TransitionCanvas.enabled = false;
        fadeActive = false;
        win = false;
        dir = 1;
        videoPlayer.loopPointReached += ReachedLoop;
    }

    void Update()
    {
        if (fadeActive)
        {
            HandleFade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Debug.Log("pressed 1");
            collectionManager.CollectGem("Fire");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Debug.Log("pressed 2");
            collectionManager.CollectGem("Water");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Debug.Log("pressed 3");
            collectionManager.CollectGem("Grass");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Debug.Log("pressed 4");
            collectionManager.CollectGem("Light");
        }
        if (!win && collectionManager.Win())
        {
            TransitionCanvas.enabled = true;
            Debug.Log("WINWIN");
            PlayerStatusCanvas.SetActive(false);
            videoPlayer.Play();
            Invoke("winwin", 1.3f);  
            fadeActive = true;
            win = true;    
        }
    }

    void HandleFade()
    {
        Debug.Log("hehe");
        Color color = TransitionMask.GetComponent<Image>().color;
        color.a += 0.4f * (1/(color.a + 0.1f)) * dir * Time.deltaTime;
        if (color.a > 0.99f){
            dir = -1;
        }
        if (color.a < 0.01 && dir == -1) {
            fadeActive = false;
            color.a = 0f;
        }
        // color.a = 1;
        TransitionMask.GetComponent<Image>().color = color;
    }

    void winwin()
    {
        EndCanvas.SetActive(true);
        
    }

    void ReachedLoop(VideoPlayer videoplayer)
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Single);
        // PlayerPrefs.SetString("sceneStatus", "Main");
    }
}
