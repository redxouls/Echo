using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance{get; private set; }

    public enum CheckStatus
    {
        NotChecking = 0, 
        Checking = 1, 
        Checked = 2,
    }

    public enum KeyCheckMap
    {
        W = 0,
        A = 1,
        S = 2,
        D = 3,
    }

    public bool paused;
    public bool tutorial_finished;
    public bool player_can_move;
    public CheckStatus space_check;
    public CheckStatus move_check;
    public CheckStatus grenade_check;
    public CheckStatus red_warning_check;
    public CheckStatus collect_gem_check;

    public Image image_W;
    public Image image_A;
    public Image image_S;
    public Image image_D;

    private int grenade_check_flag;
    private bool[] move_check_flag = new bool[4]; // [w, a, s, d]
    private float canvas_fade_out_t;
    private bool red_user_check;
    private bool gem_user_check;

    public CanvasGroup redcanvasGroup;
    public CanvasGroup gemcanvasGroup;

    public Canvas SpaceTutor;
    public Canvas GrenadeTutorAim;
    public Canvas GrenadeTutorLaunch;
    public Canvas MoveTutor;
    public Canvas RedWarningTutor;
    public Canvas CollectGemTutor;

    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        paused = false;
        initializeTutorial();
        initializeCheckStatus();
        
        grenade_check_flag = 0;
        canvas_fade_out_t = -1f;
        red_user_check = false;
        gem_user_check = false;
        for (int i  = 0; i < 4; i++) move_check_flag[i] = false;
        ResetAllCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorial_finished)
        {
            if (!paused)
            {
                // tutorial_finished = CheckTutorial();
                SpaceCheck();
                GrenadeCheck();
                MoveCheck();
                RedWarningCheck();
                CollecGemCheck();
            }
            else
            {
                ResetAllCanvas();
            }
        }
        else
        {
            PlayerPrefs.SetInt("TutorFinished", 1);
        }
    }

    void SpaceCheck()
    {
        if (space_check == CheckStatus.Checking)
        {
            SpaceTutor.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                space_check = CheckStatus.Checked;
                grenade_check = CheckStatus.Checking;
                SpaceTutor.enabled = false;
            }
        }
    }

    void GrenadeCheck()
    {
        if (grenade_check == CheckStatus.Checking)
        {
            if (grenade_check_flag == 0)
            {
                GrenadeTutorAim.enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    grenade_check_flag = 1;
                    GrenadeTutorAim.enabled = false;
                }
            }
            else if (grenade_check_flag == 1)
            {
                GrenadeTutorLaunch.enabled = true;
                if (Input.GetMouseButtonUp(0))
                {
                    GrenadeTutorLaunch.enabled = false;
                    grenade_check = CheckStatus.Checked;
                    move_check = CheckStatus.Checking;
                }
            }
        }
    }

    void MoveCheck()
    {
        if (move_check == CheckStatus.Checking)
        {
            MoveTutor.enabled = true;
            if (Input.GetKeyDown(KeyCode.W))
            {
                move_check_flag[(int)KeyCheckMap.W] = true;
                ShowPressedKey(image_W);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                move_check_flag[(int)KeyCheckMap.A] = true;
                ShowPressedKey(image_A);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                move_check_flag[(int)KeyCheckMap.S] = true;
                ShowPressedKey(image_S);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                move_check_flag[(int)KeyCheckMap.D] = true;
                ShowPressedKey(image_D);
            }
            if (move_check_flag[(int)KeyCheckMap.W]&&move_check_flag[(int)KeyCheckMap.A]&&move_check_flag[(int)KeyCheckMap.S]&&move_check_flag[(int)KeyCheckMap.D])
            {
                MoveTutor.enabled = false;
                move_check = CheckStatus.Checked;
            }
        }
    }

    void RedWarningCheck()
    {
        if (red_warning_check == CheckStatus.Checking)
        {
            RedWarningTutor.enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (red_user_check)
            {
                canvas_fade_out_t = 0f;
                red_warning_check = CheckStatus.Checked;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (canvas_fade_out_t >= 0f && red_warning_check == CheckStatus.Checked)
        {
            canvas_fade_out_t += Time.deltaTime;
            float alpha = redcanvasGroup.alpha;
            alpha = 1 - Mathf.Pow(canvas_fade_out_t, 3);
            redcanvasGroup.alpha = alpha;
            if (canvas_fade_out_t >= 2.9f)
            {
                RedWarningTutor.enabled = false;
                canvas_fade_out_t = -1f;
            }
        }
    }

    void CollecGemCheck()
    {
        if (collect_gem_check == CheckStatus.Checking)
        {
            CollectGemTutor.enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (gem_user_check)
            {
                canvas_fade_out_t = 0f;
                collect_gem_check = CheckStatus.Checked;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (canvas_fade_out_t >= 0f && collect_gem_check == CheckStatus.Checked)
        {
            // Debug.Log(canvas_fade_out_t);
            canvas_fade_out_t += Time.deltaTime;
            float alpha = gemcanvasGroup.alpha;
            alpha = 1 - Mathf.Pow(canvas_fade_out_t, 3);
            gemcanvasGroup.alpha = alpha;
            if (canvas_fade_out_t >= 2.9f)
            {
                CollectGemTutor.enabled = false;
                tutorial_finished = true;
                canvas_fade_out_t = -1f;
            }
        }
    }

    // bool CheckTutorial()
    // {
    //     return (space_check == CheckStatus.Checked)&&(move_check == CheckStatus.Checked)&&(grenade_check == CheckStatus.Checked)&&(red_warning_check == CheckStatus.Checked)&&(collect_gem_check == CheckStatus.Checked);
    // }

    void initializeTutorial()
    {
        if (PlayerPrefs.HasKey("TutorFinished"))
        {
            tutorial_finished = (PlayerPrefs.GetInt("TutorFinished") == 1) ? true : false;
            tutorial_finished = false;
        }
        else
        {
            // test
            tutorial_finished = false;
        }
    }  

    void initializeCheckStatus()
    {
        space_check = CheckStatus.Checking;
        grenade_check = CheckStatus.NotChecking;
        move_check = CheckStatus.NotChecking;
        red_warning_check = CheckStatus.NotChecking;
        collect_gem_check = CheckStatus.NotChecking;
    }

    void ResetAllCanvas()
    {
        SpaceTutor.enabled = false;
        GrenadeTutorAim.enabled = false;
        GrenadeTutorLaunch.enabled = false;
        MoveTutor.enabled = false;
        RedWarningTutor.enabled = false;
        CollectGemTutor.enabled = false;
    }

    void ShowPressedKey(Image img)
    {
        var color = img.color;
        color.a = 255;
        img.color = color;
    }

    public void RedCheck()
    {
        red_user_check = true;
    }

    public void GemCheck()
    {
        gem_user_check = true;
    }

}
