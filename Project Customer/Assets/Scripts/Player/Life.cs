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
    float fireDamageRate = 2;
    float normalFireDamageRate;

    bool canTakeDamage = true;
    bool triggerOnce;

    public float oxygen = 100;
    float maxOxygen;
    public float oxygenRundownSpeed = 0.5f;

    [SerializeField]
    float oxygenRundownMaxSpeed = 0.75f;
    float originalOxygenRundownSpeed;

    public bool damageActivated = true;

    [SerializeField]
    int flameCountToMaxLoss = 5;

    AudioManager audioManager;
    bool playOnce;
    string[] cough = { "Cough1", "Cough2", "Cough3" };
    string heavyBreathing = "HeavyBreathing";
    int breathingStage = 4;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        maxOxygen = oxygen;
        originalOxygenRundownSpeed = oxygenRundownSpeed;
        normalFireDamageRate = fireDamageRate;
    }

    // Update is called once per frame
    void Update()
    {
        OxygenStateSound();
        SetOxygenInterval();
        Timer();
        OxygenRundown();

        if (life <= 0)
        {
            if (onDeath != null && !triggerOnce)
            {
                onDeath();
                triggerOnce = true;
            }
        }
    }

    private void SetOxygenInterval()
    {
        if(Fire.flameCount > 1)
        {
            oxygenRundownSpeed = Mathf.Lerp(originalOxygenRundownSpeed, oxygenRundownMaxSpeed, (float)Fire.flameCount / (float)flameCountToMaxLoss);
        }
        else if(Fire.flameCount == 1)
        {
            oxygenRundownSpeed = originalOxygenRundownSpeed;
        }
        else
        {
            oxygenRundownSpeed = 0;
        }
    }
    
    private void OnParticleCollision(GameObject other)
    {
        if (damageActivated && other.gameObject.tag == "Fire")
        {
            if (canTakeDamage && life > 0)
            {
                life -= fireDamage;
                canTakeDamage = false;
            }
        }
    }

    void Timer()
    {
        if (!canTakeDamage)
        {
            fireDamageRate -= Time.deltaTime;
            if (fireDamageRate < 0)
            {
                fireDamageRate = normalFireDamageRate;
                canTakeDamage = true;
            }
        }
    }

    void OxygenStateSound()
    {
        float currentPercentage = oxygen / maxOxygen * 100;
        Debug.Log(currentPercentage);

        if (currentPercentage <= (20 * breathingStage))
        {
            breathingStage--;
            int randomNumber = UnityEngine.Random.Range(0, 3);

            audioManager.Play(cough[randomNumber]);
        }

        if(currentPercentage <= 10 && !playOnce)
        {
            playOnce = true;
            audioManager.Play(heavyBreathing);
        }
    }

    void OxygenRundown()
    {
        if(damageActivated)
        {
            oxygen -= Time.deltaTime * oxygenRundownSpeed;
            if (oxygen <= 0)
            {
                audioManager.Stop(heavyBreathing);
                life = 0;
            }
        }
    }

    public int GetLife()
    {
        return life;
    }

    public int GetOxygen()
    {
        return (int)oxygen;
    }
}
