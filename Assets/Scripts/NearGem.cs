using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearGem : MonoBehaviour
{
    public ActivateGem Gem;
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !Gem.triggered)
        {
            Gem.triggered = true;
        }
    }
}
