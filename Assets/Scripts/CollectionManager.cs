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
    private CanvasGroup canvasGroup;
    private float easefunc_t;
    private int fadeStatus; // 0: steady, 1: fade out, 2: fade in

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < collectionGems.Length; i++)
        {
            collectionGems[i] = false;
            completeGems[i] = false;
        }
        gems.Add(gem1);
        gems.Add(gem2);
        gems.Add(gem3);
        gems.Add(gem4);
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        easefunc_t = 0f;
        fadeStatus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < collectionGems.Length; i++)
        {
            if (collectionGems[i] && fadeStatus == 1 && easefunc_t > 0.2)
            {
                var color = gems[i].color;
                if (color.a < 1f)
                {
                    color.a += (1f / color.a) * 0.005f * Time.deltaTime;
                    gems[i].color = color;
                }
            }
        }

        if (fadeStatus != 0)
        {
            easefunc_t += 0.4f * Time.deltaTime;
            // Debug.Log(easefunc_t);
            float alpha = canvasGroup.alpha;
            if (fadeStatus == 2)
            {
                // alpha = 1 - Mathf.Pow(1 - easefunc_t, 3);
                alpha = Mathf.Sqrt(1 - Mathf.Pow(easefunc_t - 1, 2));
                if (alpha > 0.99f)
                {
                    fadeStatus = 1;
                    easefunc_t = 0f;
                }
            }
            else if (fadeStatus == 1)
            {
                // alpha = 1 - Mathf.Pow(easefunc_t, 3);
                alpha = Mathf.Sqrt(1 - Mathf.Pow(easefunc_t, 2));
                if (alpha < 0.01f)
                {
                    fadeStatus = 0;
                    easefunc_t = 0f;
                }
            }
            // alpha += (1f / alpha) * 0.005f * Time.deltaTime;
            canvasGroup.alpha = alpha;
        }

        // TODO: effects of putting gems to the end doors
        for (int i = 0; i < completeGems.Length; i++)
        {
            if (!completeGems[i])
                continue;
            var color = gems[i].color;
            color.a = 0.2f;
            gems[i].color = color;
            // if (color.a > 0.02f)
            // {
            //     color.a -= (1f / (1.01f - color.a)) * 0.5f * Time.deltaTime;
            //     gems[i].color = color;
            // }
            // else
            // {
            //     color.a = 0.02f;
            //     gems[i].color = color;
            //     Debug.Log("haha");
            // }
        }

    }

    public void CollectGem(string gemName)
    {
        // Debug.Log("collect "+gemName+" gem.");
        if (gemName == "Fire")
        {
            collectionGems[0] = true;
            fadeStatus = 2;
            easefunc_t = 0f;
        }
        else if (gemName == "Water")
        {
            collectionGems[1] = true;
            fadeStatus = 2;
            easefunc_t = 0f;
        }
        else if (gemName == "Grass")
        {
            collectionGems[2] = true;
            fadeStatus = 2;
            easefunc_t = 0f;
        }
        else if (gemName == "Light")
        {
            collectionGems[3] = true;
            fadeStatus = 2;
            easefunc_t = 0f;
            if (TutorialManager.Instance.red_warning_check == TutorialManager.CheckStatus.Checked && TutorialManager.Instance.collect_gem_check == TutorialManager.CheckStatus.NotChecking)
            {
                TutorialManager.Instance.collect_gem_check = TutorialManager.CheckStatus.Checking;
            }
        }
        else
        {
            Debug.Log("Invalid gemName");
        }
        //test for tutor (should removed in the build version)
        // Debug.Log(TutorialManager.Instance.red_warning_check);
        // Debug.Log(TutorialManager.Instance.collect_gem_check);

    }

    public void CompleteGem(string gemName)
    {
        Debug.Log("complete " + gemName + " gem.");
        if (gemName == "Fire")
        {
            collectionGems[0] = false;
            completeGems[0] = true;
        }
        else if (gemName == "Water")
        {
            collectionGems[1] = false;
            completeGems[1] = true;
        }
        else if (gemName == "Grass")
        {
            collectionGems[2] = false;
            completeGems[2] = true;
        }
        else if (gemName == "Light")
        {
            collectionGems[3] = false;
            completeGems[3] = true;
        }
        else
        {
            Debug.Log("Invalid gemName");
        }
    }
    public int GetGemCollection()
    {
        int ret = 0;
        for (int i = 0; i < 4; ++i)
        {
            int temp = collectionGems[i] ? 1 : 0;  // because cannot convert bool to int
            ret += (temp << i);
        }
        return ret;
    }

    public bool Win()
    {
        return completeGems[0] && completeGems[1] && completeGems[2] && completeGems[3];
    }
}
