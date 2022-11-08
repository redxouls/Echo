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
    protected Vector3 position;
    protected WAVE_ATTRIBUTE attribute;

    public Wave()
    {
        attribute = WAVE_ATTRIBUTE.DEAD;
    }

    public void Init(float thickness, float lifeSpan, float speed, Vector3 position, WAVE_ATTRIBUTE attribute)
    {
        this.age = 0;
        this.thickness = thickness;
        this.lifeSpan = lifeSpan;
        this.speed = speed;
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
        return speed * age;
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
