using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public static event Action onRepellSoda;

    public static int flameCount =0;

    bool hasBeenAdded;

    AudioManager audioManager;

    public int maxLife = 5;
    [SerializeField]
    int life = 5;
    [SerializeField]
    int fireLifeFilledBucket = 5;

    [SerializeField]
    bool electricFire = false;
    [SerializeField]
    bool cookingFire = false;

    [SerializeField]
    float hitCooldown = 0.75f;
    float lastHit;

    [SerializeField]
    float growthCooldown = 20;
    float lastGrowth;

    [SerializeField]
    float spreadCooldown = 120;
    float lastSpread;

    // Higher values make the flames bigger...
    [SerializeField]
    float flameMaxHeight = 1.48f;

    WaterInteractable waterInteractable;

    AudioSource audioSource;
    SoundTransition sTrans;

    ParticleSystem fire;
    bool extinguished;

    // For firespreading...
    [SerializeField]
    Fire[] fireSpread;
    bool setSpreadFireToMax;

    [SerializeField]
    ParticleSystem smoke;

    [SerializeField]
    Transform lighting;

    int lastLife;
    List<Fire> spawnedFiresTracker = new List<Fire>();

    [SerializeField]
    AudioClip bigFire;
    [SerializeField]
    AudioClip smallFire;


    // Use this Life property instead of the "life" variable...
    public int Life
    {
        get { return life; }
        set
        {
            lastLife = life;
            life = value;
        }
    }
    

    void Start()
    {
        sTrans = GetComponent<SoundTransition>();
        audioSource = GetComponent<AudioSource>();
        fire = GetComponent<ParticleSystem>();
        waterInteractable = FindObjectOfType<WaterInteractable>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        AddToCount();
        CheckSound();
        FlameExtinguished();
        SpreadFire();
        FireGrowth();
        AddLifeOverTime();

        //Testing();
    }

    private void AddToCount()
    {
        if (!hasBeenAdded)
        {
            flameCount++;
            hasBeenAdded = true;
        }
    }

    private void CheckSound()
    {
        if (!audioSource.isPlaying && audioSource.enabled)
        {
            audioSource.volume = 0;

            if(Life > 2) audioSource.clip = bigFire;
            else audioSource.clip= smallFire;

            audioSource.Play();
            sTrans.TransitionToFullVolume();
        }
    }

    private void Testing()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Life++;
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            Life--;
        }
    }

    private void FireGrowth()
    {
        ParticleSystem.MainModule pSMain = fire.main;
        float lifeValue = (float)life / (float)maxLife;
        pSMain.startLifetime = Mathf.Lerp(1.1f, flameMaxHeight, lifeValue);
    }

    private void AddLifeOverTime()
    {
        if (Life < maxLife && Time.time - lastGrowth > growthCooldown)
        {
            Life++;
            lastGrowth = Time.time;
        }
        else if (Life > maxLife) Life = maxLife;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "ElectricFireStop")
        {
            if (electricFire)
            {
                Life = 0;
            }
            // Baking soda used on anything else...
            else
            {
                // Repell the baking soda...
                if (onRepellSoda != null)
                {
                    onRepellSoda();
                }
            }
        }

        if (Time.time - lastHit > hitCooldown)
        {
            if (other.gameObject.tag == "FoamBullet")
            {
                Debug.Log("gawk");
                Life--;
            }
            if (other.GetComponent<WaterInteractable>())
            {
                waterInteractable = other.GetComponent<WaterInteractable>();
                if (!waterInteractable.GetPotionStatus())
                {
                    if (Life <= fireLifeFilledBucket ) //water bucket
                    {
                        DifferentFireCheck();
                    }
                }
            }

            lastHit = Time.time;
        }
    }

    private void DifferentFireCheck()
    {
        if (!electricFire && !cookingFire)
        {
            Life = 0;
            waterInteractable.Dry();
        }
        // If it's an electricFire or cookingFire...
        else
        {
            // Explosion!!!
            audioManager.Play("FireFail");
            Life = maxLife;
            setSpreadFireToMax = true;
        }
    }




    private void FlameExtinguished()
    {
        if (Life <= 0)
        {
            sTrans.TransitionToZeroVolume();
            fire.Stop();
            smoke.Stop();
            extinguished = true;
        }
        else if(life > 0 && extinguished && (!fire.isPlaying || !smoke.isPlaying))
        {
            fire.Play();
            smoke.Play();
            transform.parent.gameObject.SetActive(true);
            extinguished = false;
        }

        if(!fire.isPlaying)
        {
            flameCount--;
            hasBeenAdded = false;
            audioSource.Stop();
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void SpreadFire()
    {
        KeepTrackOfInstances();
        if (fireSpread.Length > 0 && Life == maxLife && lastLife < Life && spawnedFiresTracker.Count <= 0 && Time.time - lastSpread > spreadCooldown)
        {
            for (int i = 0; i < fireSpread.Length; i++)
            {
                lastSpread = Time.time;
                fireSpread[i].transform.parent.gameObject.SetActive(true);
                spawnedFiresTracker.Add(fireSpread[i]);
            }
        }

        if (setSpreadFireToMax)
        {
            SetSpreadFireToMax();
        }
    }

    private void KeepTrackOfInstances()
    {
        for (int i = 0; i < spawnedFiresTracker.Count; i++)
        {
            if (!spawnedFiresTracker[i].isActiveAndEnabled)
            {
                // Set back Life to 1 so that it has life in case it respawns...
                spawnedFiresTracker[i].Life = 1;
                spawnedFiresTracker.Remove(spawnedFiresTracker[i]);
            }
        }
    }

    private void SetSpreadFireToMax()
    {
        for (int i = 0; i < spawnedFiresTracker.Count; i++)
        {
            spawnedFiresTracker[i].Life = spawnedFiresTracker[i].maxLife;
        }

        setSpreadFireToMax = false;
    }
}
