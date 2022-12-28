using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMagager : MonoBehaviour
{
    public Transform originRespawn;
    public Transform altarRespawn;
    public GameObject player;
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
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = respawn.position;
        player.transform.rotation = respawn.rotation;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
