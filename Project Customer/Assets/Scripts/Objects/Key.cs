using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    private PhysicsPickup player;
    void Start()
    {
        player = FindObjectOfType<PhysicsPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Unlockable")
        {
            //player.objectName = null;
            player.currentObject = null;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
