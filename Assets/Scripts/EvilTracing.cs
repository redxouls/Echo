using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvilTracing : MonoBehaviour
{
    public SoundWaveManager soundWaveManager;
    public ParticleSystem particleSystem;
    public NavMeshAgent agent;
    public float trigger_dst = 5.0f;
    private NavMeshHit target;
    private NavMeshPath path;
    private bool moving;
    private float route_length;
    // Start is called before the first frame update
    void Start() {
        // InvokeRepeating("Trace", 1f, 0.1f);
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        route_length = 0.0f;
        moving = false;
    }

    // Update is called once per frame
    void Update() {
        if(!PauseController.GamePaused)
        {
            VisualizeEvil();
            Trace();    
        }
        
    }
    void Trace() {
        Vector3 wavePoint;
        if (HasTarget(out wavePoint)) {
            // code vector4 points to vector3 position
            NavMesh.SamplePosition(wavePoint, out target, 5.0f, NavMesh.AllAreas);
            // Debug.Log(moving);
            // agent.SetDestination(position);
            if(NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path)) {
                route_length = CalcPathDistance(path);
                // Debug.Log(route_length);          
                if (route_length <= trigger_dst) {
                    moving = true;
                    agent.SetPath(path);
                }
                else {
                    moving = false;
                }
            }
        }
    }
    void VisualizeEvil() {
        if (moving)
        {
            particleSystem.Play();
            // Debug.Log("moving");
        }
        else
        {
            particleSystem.Stop();
            // Debug.Log("stop");
        }
    }
    float CalcPathDistance(NavMeshPath path) {
        float route_length = 0.0f;
        for (int i = 0; i < path.corners.Length - 1; i++) {
            route_length += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }
        return route_length;
    }

    bool HasTarget(out Vector3 target)
    {
        target = Vector3.zero;
        float minDistance = float.MaxValue;
        bool found = false;
        for (int i = 0; i < soundWaveManager.maxNumOfWave; ++i) 
        {
            WAVE_ATTRIBUTE attribute = soundWaveManager.waves[i].GetAttribute();
            if (attribute == WAVE_ATTRIBUTE.PLAYER || attribute == WAVE_ATTRIBUTE.GRENADE)
            {
                float distance = Vector3.Distance(soundWaveManager._Points[i], transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = soundWaveManager._Points[i];
                    found = true;
                }
            }
        }
        return found;
    }
}