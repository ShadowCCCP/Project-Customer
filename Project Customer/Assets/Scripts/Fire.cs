using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    int life = 5;

    void Start()
    {
        
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
    }
    
}
