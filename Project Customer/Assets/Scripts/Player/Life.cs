using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public static event Action onDeath;

    [SerializeField]
    private int life = 100;

    [SerializeField]
    private int fireDamage = 20;

    [SerializeField]
    float fireDamageRate = 1;

    bool canTakeDamage = true;
    bool triggerOnce;

    public float Oxygen = 100;
    public float OxygenRundownSpeed = 0.5f;

    //private float fireDamageRate = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer();
        oxygenRundown();
    }
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Fire")
        {
            if (canTakeDamage && life > 0)
            {
                life -= fireDamage;
                canTakeDamage = false;
            }

            if (life <= 0)
            {
                if (onDeath != null && !triggerOnce)
                {
                    onDeath();
                    triggerOnce = true;
                }
            }
        }
    }

    void timer()
    {
        if (!canTakeDamage)
        {
            fireDamageRate -= Time.deltaTime;
            if (fireDamageRate < 0)
            {
                fireDamageRate = 2;
                canTakeDamage = true;
            }
        }
    }

    void oxygenRundown()
    {
        Oxygen -= Time.deltaTime * OxygenRundownSpeed;
        if (Oxygen <= 0)
        {
            life = 0;
        }
    }

    public int GetLife()
    {
        return life;
    }

    public int GetOxygen()
    {
        return (int)Oxygen;
    }
}
