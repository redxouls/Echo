using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorRedWarningTrigger : MonoBehaviour
{
    bool triggered = false;
    void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.tag == "Player" && !triggered)
        {
            triggered = true;
            TutorialManager.Instance.red_warning_check = TutorialManager.CheckStatus.Checking;
        }
        // Debug.Log(collisionInfo.tag);
    }
}
