using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CollectionManager collectionManager;
    public Canvas EndCanvas;
    // Start is called before the first frame update
    
    void Start()
    {
        EndCanvas.enabled = false;
    }

    void Update()
    {
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

        if (collectionManager.Win())
        {
            Debug.Log("WINWIN");
            EndCanvas.enabled = true;

        }
    }

    
}
