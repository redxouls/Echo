using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvilTracing : MonoBehaviour
{
    public SoundWaveManager soundWaveManager;
    public ParticleSystem particleSystem;
    public NavMeshAgent agent;
    public float triget_dst = 10.0f;
    private Vector3 target;
    private NavMeshPath path;
    private bool moving = false;
    private float route_length = 0.0f;
    // Start is called before the first frame update
    void Start() {
        // InvokeRepeating("Trace", 1f, 0.1f);
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        route_length = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        // VisualizeEvil();
        // code vector4 points to vector3 position
        target = soundWaveManager.points[soundWaveManager.endIndex - 1];
        agent.SetDestination(target);
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
        route_length = 0.0f;
        for (int i = 0; i < path.corners.Length - 1; i++) {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 2);
            route_length += (path.corners[i] - path.corners[i + 1]).sqrMagnitude;
            // if (route_length > triget_dst) {
            //     break;
            // }
        }
        Debug.Log("route_length");
        Debug.Log(route_length);
        Debug.Log("target");
        Debug.Log(target);
    }
    // void Trace() {
        // NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
        // route_length = 0.0f;
        // for (int i = 0; i < path.corners.Length - 1; i++) {
        //     Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        //     route_length += (path.corners[i] - path.corners[i + 1]).sqrMagnitude;
            // if (route_length > triget_dst) {
            //     break;
            // }
        // }
        // Debug.Log(route_length);
        // if (route_length <= triget_dst){
        //     moving = true;
        //     agent.SetDestination(target.transform.position);
        // }
        // else {
        //     moving = false;
        // }
    // }
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
}
