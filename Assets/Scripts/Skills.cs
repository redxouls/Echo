using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public float reloadSpeed = 1.0f;
    public float thickness = 2.0f;
    public float lifeSpan = 5.0f;
    public float speed = 2f;

    public SoundWaveManager soundWaveManager;
    public AudioClip SkillSteps;

    private float pressedDuration;
    private Transform Trail;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        Trail = transform.Find("LightSource");
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseController.GamePaused)
        {
            Clap();
        }
    }

    void Clap()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (timer < reloadSpeed) return;
            AudioManager.Instance.PlayAudioClip(SkillSteps, "footstep");
            soundWaveManager.AddWave(thickness, lifeSpan, speed, 1, Trail.position, WAVE_ATTRIBUTE.PLAYER);
            timer = 0.0f;
        }
        timer += Time.deltaTime;
    }
}