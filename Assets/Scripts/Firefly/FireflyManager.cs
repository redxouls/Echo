using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FireflyManager : MonoBehaviour
{   
    public PathCreator[] pathCreators;
    public float speed;

    private float distanceTravelled;
    private FireflyLight fireflyLight;
    private FireflyParticleSystem fireflyParticleSystem;
    private bool moveToNext;
    private int currentPathIndex;

    // Start is called before the first frame update
    void Start()
    {
        fireflyLight = transform.GetChild(0).GetComponent<Light>().GetComponent<FireflyLight>();
        fireflyParticleSystem = transform.GetChild(1).GetComponent<ParticleSystem>().GetComponent<FireflyParticleSystem>();

        moveToNext = false;
        currentPathIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToNext)
            MoveToDesitination(currentPathIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && !moveToNext)
        {
            // fireflyParticleSystem.state = FireflyParticleSystem.State.Shrink;
            // transform.position += new Vector3(5.0f, 0.0f, 5.0f);
            // fireflyLight.SetOrigin(transform.position);
            Debug.Log(other.name);
            moveToNext = true;
        }
    }

    private void MoveToDesitination(int pathCreatorIndex)
    {
        PathCreator pathCreator = pathCreators[pathCreatorIndex];
        distanceTravelled += speed * Time.deltaTime;
        if (distanceTravelled >= pathCreator.path.length)
        {
            moveToNext = false;
            distanceTravelled = 0.0f;
            currentPathIndex += 1;
            return;
        }
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        fireflyLight.SetOrigin(transform.position);
    }
}
