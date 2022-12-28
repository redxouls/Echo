using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform originRespawn;
    public Transform altarRespawn;
    public PlayerMovement player;
    public GameObject deathScreen;
    public CollectionManager collectMgr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        
        Transform respawn;
        if (collectMgr.collectionGems[3] || collectMgr.completeGems[3]) // Light gem's index is 3
        {
            respawn = altarRespawn;
        }
        else
        {
            respawn = originRespawn;
        }
        player.isDead = false;
        player.gameObject.GetComponent<CharacterController>().enabled = false;
        player.gameObject.transform.position = respawn.position;
        player.gameObject.transform.rotation = respawn.rotation;
        player.gameObject.GetComponent<CharacterController>().enabled = true;
        deathScreen.SetActive(false);
    }
}
