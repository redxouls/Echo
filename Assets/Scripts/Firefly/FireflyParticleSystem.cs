using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyParticleSystem : MonoBehaviour
{   
    public enum State: short
    {
        Still = 0,
        Spread = 1,
        Shrink = 2,
        Move = 3,
    }

    public State state;
    private new ParticleSystem particleSystem;
    private ParticleSystem.MainModule main;
    ParticleSystem.MinMaxCurve startSpeed;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Still;
        particleSystem = GetComponent<ParticleSystem>();
        main = particleSystem.main;
        startSpeed = main.startSpeed;
    }

    // Update is called once per frame
    void Update()
    {   

    }

    void Shrink()
    {
        startSpeed = new ParticleSystem.MinMaxCurve(main.startSpeed.constant - 1 * Time.deltaTime);
        main.startSpeed = startSpeed;

        if (main.startSpeed.constant < 0)
        {
            state = State.Still;
        }
    }

    void Spread()
    {

    }

    void Move()
    {
        Debug.Log("moveing");
    }
}
