using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public static event Action onTeleport;

    [SerializeField]
    Transform placeToTeleport;

    PhysicsPickup playerModel;

    void Start()
    {
        playerModel = FindObjectOfType<PhysicsPickup>();
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
            if(onTeleport != null) onTeleport();
        }
    }
}
