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
        // switch (state)
        // {
        //     case (State.Still):
        //         break;
        //     case (State.Spread):
        //         Spread();
        //         break;
        //     case (State.Shrink):
        //         Shrink();
        //         break;
        //     case (State.Move):
        //         Move();
        //         break;
        //     default:
        //         break;
        // }
    }

    void Shrink()
    {
        startSpeed = new ParticleSystem.MinMaxCurve(main.startSpeed.constant - 1 * Time.deltaTime);
        main.startSpeed = startSpeed;

        if (main.startSpeed.constant < 0)
        {
            Debug.Log("Shrink Ended");
            state = State.Still;
        }
        Debug.Log(main.startSpeed.constant);
    }

    void Spread()
    {
        // var startSpeed = main.startSpeed;
        // startSpeed.constant += 1 * Time.deltaTime;
        // if (startSpeed.constant > 3f)
        // {
        //     Debug.Log("Shrinked");
        //     state = State.Still;
        // }
        // Debug.Log(startSpeed.constant);
    }

    void Move()
    {
        Debug.Log("moveing");
    }
}
