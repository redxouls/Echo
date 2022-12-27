using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WAVE_ATTRIBUTE {
    DEAD = 0,
    PLAYER = 1,
    GRENADE = 2,
    PASSIVE = 3
}

public class Wave
{
    protected float age;
    protected float thickness;
    protected float lifeSpan;
    protected float speed;
    protected float alphaAttenuation;
    protected Vector3 position;
    protected WAVE_ATTRIBUTE attribute;

    public Wave()
    {
        attribute = WAVE_ATTRIBUTE.DEAD;
    }

    public void Init(float thickness, float lifeSpan, float speed, float alphaAttenuation, Vector3 position, WAVE_ATTRIBUTE attribute)
    {
        this.age = 0;
        this.thickness = thickness;
        this.lifeSpan = lifeSpan;
        this.speed = speed;
        this.alphaAttenuation = alphaAttenuation;
        this.position = position;
        this.attribute = attribute;
    }

    public void Update()
    {
        if (attribute == WAVE_ATTRIBUTE.DEAD)
        {
            return;
        }
        age += Time.deltaTime;
        if (age > lifeSpan)
        {
            attribute = WAVE_ATTRIBUTE.DEAD;
        }
    }

    public float GetRadius()
    {
        return speed * GetAge();
    }

    public float GetAlphaAttenuation()
    {
        return alphaAttenuation * (1 - EaseInQuint(age / lifeSpan));
    }

    private float GetAge()
    {
        return age;
        // return age * EaseOutSine(age / lifeSpan);
    }

    private float EaseOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }

    private float EaseOutCubic(float x)
    {
        return 1 - Mathf.Pow(1 - x, 3);
    }

    private float EaseInQuint(float x)
    {
        return x * x * x * x * x;
    }

    public WAVE_ATTRIBUTE GetAttribute()
    {
        return attribute;
    }

    public bool IsDead()
    {
        return attribute == WAVE_ATTRIBUTE.DEAD;
    }
}



