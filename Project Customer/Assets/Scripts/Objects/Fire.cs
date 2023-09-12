using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    int maxLife = 5;
    [SerializeField]
    int life = 5;
    [SerializeField]
    int fireLifeFilledBucket = 5;
    //[SerializeField]
    // int fireLifeBlanket = 5;
    [SerializeField]
    int fireLifeWetBlanket = 5;

    [SerializeField]
    bool electricFire = false;
    [SerializeField]
    bool cookingFire = false;

    [SerializeField]
    float cooldown = 0.75f;
    float lastHit;

    // Higher values make the flames bigger...
    [SerializeField]
    float maxLifeTime = 1.48f;

    WaterInteractable waterInteractable;

    ParticleSystem fire;
    bool extinguished;

    // For firespreading...
    [SerializeField]
    Fire[] fireSpread;

    int lastLife;
    List<Fire> spawnedFiresTracker = new List<Fire>();

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
        fire = GetComponent<ParticleSystem>();
        waterInteractable = FindObjectOfType<WaterInteractable>();
    }

    void Update()
    {
        FlameExtinguished();
        SpreadFire();
        FireGrowth();

        Testing();
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
        pSMain.startLifetime = Mathf.Lerp(1.1f, maxLifeTime, lifeValue);
    }

    private void OnParticleCollision(GameObject other)
    {
        if(Time.time - lastHit > cooldown)
        {
            if (other.gameObject.tag == "FoamBullet")
            {
                Life--;
            }
            if (other.GetComponent<WaterInteractable>())
            {
                waterInteractable = other.GetComponent<WaterInteractable>();
                if (!waterInteractable.trueIfPotion)
                {
                    /*if (life <= fireLifeEmptyBucket && !waterInteractable.GetWetStatus()) //empty bucket
                    {
                        life = 0;
                        //Debug.Log("empty extinguished");
                    }*/
                    if (Life <= fireLifeFilledBucket && waterInteractable.GetWetStatus() ) //water bucket
                    {
                        ElectricFireCheck();
                        CookingFireCheck();
                        //life = 0;
                        //Debug.Log("filled extinguished");
                        //waterInteractable.Dry();
                    }

                }
                else
                {
                    if (!waterInteractable.GetWetStatus()) // dry blanket
                    {
                        //destroy?
                        gameObject.SetActive(false);
                        Debug.Log("level failed");
                    }
                    else if (Life <= fireLifeWetBlanket && waterInteractable.GetWetStatus()  ) //wet blanket
                    {
                        ElectricFireCheck();
                        CookingFireCheck();
                        //life = 0;
                        //waterInteractable.Dry();
                        //Debug.Log("filled extinguished");
                    }
                }
            }

            if (other.gameObject.tag == "ElectricFireStop")
            {
                if (electricFire)
                {
                    Life = 0;
                }
                else
                {
                    gameObject.SetActive(false);
                    Debug.Log("level failed");
                }
            }

            lastHit = Time.time;
        }
    }

    private void ElectricFireCheck()
    {
        if (!electricFire)
        {
            Life = 0;
            waterInteractable.Dry();
        }
        else
        {
            Debug.Log("explosion");
        }
    }

    private void CookingFireCheck()
    {
        if (!cookingFire)
        {
            Life = 0;
            waterInteractable.Dry();
        }
        else
        {
            Debug.Log("explosion");
        }
    }

    private void FlameExtinguished()
    {
        if (Life <= 0)
        {
            fire.Stop();
            extinguished = true;
        }
        else if(life > 0 && extinguished)
        {
            fire.Play();
            extinguished = false;
        }

        if(!fire.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    private void SpreadFire()
    {
        KeepTrackOfInstances();
        if (fireSpread.Length > 0 && Life == maxLife && lastLife < Life && spawnedFiresTracker.Count == 0)
        {
            for (int i = 0; i < fireSpread.Length; i++)
            {
                fireSpread[i].gameObject.SetActive(true);
                spawnedFiresTracker.Add(fireSpread[i]);
            }
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
}
