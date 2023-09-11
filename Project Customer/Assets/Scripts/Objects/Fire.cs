using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject smoke;
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

    WaterInteractable waterInteractable;

    // For firespreading...
    [SerializeField]
    Fire[] fireSpread;

    [SerializeField]
    int maxLife = 5;

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
        waterInteractable = FindObjectOfType<WaterInteractable>();
    }

    void Update()
    {
        FlameExtinguished();
        SpreadFire();

        //Testing();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FoamBullet")
        {
            Life--;
            gameObject.SetActive(false);
        }
        if (other.GetComponent<WaterInteractable>()) 
        {
            waterInteractable = other.GetComponent<WaterInteractable>();
            if (!waterInteractable.trueIfBlanket)
            {
                /*if (life <= fireLifeEmptyBucket && !waterInteractable.GetWetStatus()) //empty bucket
                {
                    life = 0;
                    //Debug.Log("empty extinguished");
                }*/
                if (Life <= fireLifeFilledBucket && waterInteractable.GetWetStatus() && !electricFire) //water bucket
                {
                    ElectricFireCheck();
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
                else if (Life <= fireLifeWetBlanket && waterInteractable.GetWetStatus() && !electricFire) //wet blanket
                { 
                    ElectricFireCheck();
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

    private void FlameExtinguished()
    {
        if (Life <= 0)
        {
            if (smoke != null)
            {
                smoke.SetActive(true);
            }
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
