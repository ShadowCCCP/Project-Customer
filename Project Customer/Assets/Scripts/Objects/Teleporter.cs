using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Transform placeToTeleport;

    PhysicsPickup playerModel;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = FindObjectOfType<PhysicsPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pickup"))
        {
        
            playerModel.DropObject();
            other.transform.position = placeToTeleport.transform.position;
            
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerModel.DropObject();
            playerModel.gameObject.transform.position = placeToTeleport.transform.position;
        }
    }
}
