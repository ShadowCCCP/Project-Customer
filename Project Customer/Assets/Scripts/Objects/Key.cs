using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    private PhysicsPickup player;

    Safe safe;
    void Start()
    {
        player = FindObjectOfType<PhysicsPickup>();
        safe = FindObjectOfType<Safe>();
    }

    // Update is called once per frame
    void Update()
    {  
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Unlockable")
        {
            player.currentObject = null;
            safe.lockUnlocked();

            // Destroy(other.gameObject);
            Destroy(gameObject);
            
        }
    }

}
