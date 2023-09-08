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
    

    void Start()
    {
        waterInteractable = FindObjectOfType<WaterInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
            if (smoke != null)
            {
                smoke.SetActive(true);
            }
            Destroy(gameObject);
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FoamBullet")
        {
            life--;
            Destroy(other.gameObject);
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
                if (life <= fireLifeFilledBucket && waterInteractable.GetWetStatus() && !electricFire) //water bucket
                {
                    electricFireCheck();
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
                    Destroy(other.gameObject);
                    Debug.Log("level failed");
                }
                else if (life <= fireLifeWetBlanket && waterInteractable.GetWetStatus() && !electricFire) //wet blanket
                { 
                    electricFireCheck();
                    //life = 0;
                    //waterInteractable.Dry();
                    //Debug.Log("filled extinguished");
                }
            }
        }
    }

    void electricFireCheck()
    {
        if (!electricFire)
        {
            life = 0;
            waterInteractable.Dry();
        }
        else
        {
            Debug.Log("explosion");
        }
    }


}
