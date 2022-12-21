using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public ActivateGem RedGem;
    public ActivateGem BlueGem;
    public ActivateGem GreenGem;
    public ActivateGem YellowGem;
    public ActivateGem[] Gems;

    public Material[] RBGY_mat; // length = 4, order should be R, B, G, Y   

    public CollectionManager collectionMgr;

    void Start()
    {
        Gems = new ActivateGem[4];
        Gems[0] = RedGem;
        Gems[1] = BlueGem;
        Gems[2] = GreenGem;
        Gems[3] = YellowGem;
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        Debug.Log(collisionInfo.tag);
        if (collisionInfo.tag == "Player")
        {
            Debug.Log("EnterTriggerPlane");
            CompleteGem();
        }
    }

    void CompleteGem()
    {
        string[] gemName = new string[4] { "Fire", "Water", "Grass", "Light" };
        int gemCollection = collectionMgr.GetGemCollection();
        Debug.Log(gemCollection);
        for (int i = 0; i < 4; ++i)
        {
            if ((gemCollection & (1 << i)) > 0)
            {
                collectionMgr.CompleteGem(gemName[i]);
                Gems[i].timer = 0;
                Gems[i].triggered = true;
            }
        }
    }

}
