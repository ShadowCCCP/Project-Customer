using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    int life = 5;
    [SerializeField]
    int fireLifeEmptyBucket = 5;
    [SerializeField]
    int fireLifeFilledBucket = 5;

    Bucket bucket;
    

    void Start()
    {
        bucket = FindObjectOfType<Bucket>();
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
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
        if (other.GetComponent<Bucket>()) 
        {
            bucket = other.GetComponent<Bucket>();
            if(life <= fireLifeEmptyBucket && !bucket.GetFillStatus())
            {
                life = 0;
                Debug.Log("empty extinguished");
            }
            else if(life <= fireLifeFilledBucket && bucket.GetFillStatus())
            {
                life = 0;
                Debug.Log("filled extinguished");
            }
        }
    }

}
