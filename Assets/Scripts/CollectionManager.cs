using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public Image gem1;
    public Image gem2;
    public Image gem3;
    public Image gem4;

    public bool[] collectionGems = new bool[4];
    public bool[] completeGems = new bool[4];
    private List<Image> gems = new List<Image>(); 
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < collectionGems.Length; i++)
        {
            collectionGems[i] = false;
            completeGems[i] = false;
        }
        gems.Add(gem1);
        gems.Add(gem2);
        gems.Add(gem3);
        gems.Add(gem4);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < collectionGems.Length; i++)
        {
            if (collectionGems[i])
            {
                var color = gems[i].color;
                if (color.a < 1f)
                {
                    color.a += (1f / color.a) * 0.5f * Time.deltaTime;
                    gems[i].color = color;
                }
            }
        }
        // TODO: effects of putting gems to the end doors
        for (int i = 0; i < completeGems.Length; i++)
        {
            
        }

    }

    public void CollectGem(string gemName)
    {
        // Debug.Log("collect "+gemName+" gem.");
        if (gemName == "Fire")
        {
            collectionGems[0] = true;
        } 
        else if (gemName == "Water")
        {
            collectionGems[1] = true;
        }
        else if (gemName == "Grass")
        {
            collectionGems[2] = true;
        }
        else if (gemName == "Light")
        {
            collectionGems[3] = true;
        }
        else
        {
            Debug.Log("Invalid gemName");
        }
    }

    public void CompletetGem(string gemName)
    {
        Debug.Log("complete "+gemName+" gem.");
        if (gemName == "Fire")
        {
            completeGems[0] = true;
        } 
        else if (gemName == "Water")
        {
            completeGems[1] = true;
        }
        else if (gemName == "Grass")
        {
            completeGems[2] = true;
        }
        else if (gemName == "Light")
        {
            completeGems[3] = true;
        }
        else
        {
            Debug.Log("Invalid gemName");
        }
    }
}
