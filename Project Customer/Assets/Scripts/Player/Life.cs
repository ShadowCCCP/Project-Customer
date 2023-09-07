using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int life = 100;
    [SerializeField]
    private int fireDamage = 20;
    [SerializeField]
    float fireDamageRate = 1;
    bool canTakeDamage = true;
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

            /*if (life < 0)
            {
                //death
            }*/
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
