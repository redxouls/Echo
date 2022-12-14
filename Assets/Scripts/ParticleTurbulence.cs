using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[ExecuteInEditMode]
public class ParticleTurbulence : MonoBehaviour
{
    private ParticleSystem system;
    private ParticleSystem.Particle[] particles;

    [Range(0.0f, 50.0f)]
    public float Force = 10.0f;

    [Range(0.01f, 100.0f)]
    public float Frequency = 4.0f;
    public bool UseRandomSeed = true;
    public AnimationCurve OverLifetime = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f);
	public void Start()
    {
        OnScriptReload();
	}

    public void OnScriptReload()
    {
        system = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[1024];
    }

#if UNITY_EDITOR
    public void OnEnable()
    {
        OnScriptReload();
    }
#endif

    public void Update()
    {
        int particleCount = system.GetParticles(particles);
        float dt = Time.deltaTime;
        float seedFactor = UseRandomSeed ? 1.0f : 0.0f;

#if UNITY_EDITOR
        dt = 1.0f / 90.0f;
#endif

        for (int i = 0; i < particleCount; ++i)
        {
            Vector3 pos = particles[i].position;
            float force = Force;
            float freq = Frequency;

            // set random seed
            pos += Vector3.one * (float)(system.randomSeed % 1024) * seedFactor;

            // create Perlin noise on each 2D plane and map it onto the original particle space
            float noiseX = Mathf.PerlinNoise(pos.x * freq, pos.y * freq);
            float noiseY = Mathf.PerlinNoise(pos.z * freq, pos.x * freq);
            float noiseZ = Mathf.PerlinNoise(pos.y * freq, pos.z * freq);

            noiseX = noiseX * 2.0f - 1.0f;
            noiseY = noiseY * 2.0f - 1.0f;
            noiseZ = noiseZ * 2.0f - 1.0f;

            // apply force on random positions to create target velocities
            Vector3 targetVelocity = new Vector3(noiseX, noiseY, noiseZ) * force;

            // apply animation curve
            float t = 1.0f - particles[i].remainingLifetime / particles[i].startLifetime;
            float apply = OverLifetime.Evaluate(t);

            particles[i].velocity += targetVelocity * dt * apply;
        }

        system.SetParticles(particles, particleCount);
	}
}