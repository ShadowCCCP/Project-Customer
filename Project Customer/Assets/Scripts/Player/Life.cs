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

    //private float fireDamageRate = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
            if (canTakeDamage)
            {
                life -= fireDamage;
                canTakeDamage = false;
            }

            if (life <= 0)
            {
                if(onDeath != null && !triggerOnce)
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
    public int GetLife()
    {
        return life;
    }
}
