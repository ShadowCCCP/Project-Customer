using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ObjectivesScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string[] Objectives;

    PhysicsPickup physicsPickup;
    InventoryManager inventoryManager;
    CollisionCheckForObjective collisionCheckForObjective;

    [SerializeField]
    GameObject[] placesToGo;
    //walk 
    //jump
    //crouch
    //pick up object
    //hold right click to rotate obj
    //throw obj
    //use fire extinguisher

    //go to a place
    //pickup spec obj

    int index = 0;

    void Start()
    {
        physicsPickup = FindObjectOfType<PhysicsPickup>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        collisionCheckForObjective = FindObjectOfType<CollisionCheckForObjective>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (index)
        {
            case 0:
                walkObj();
                break;
            case 1:
                jumpObj(); 
                break;
            case 2:
                crouchObj();
                break;
            case 3:
                pickUpObj();
                break;
            case 4:
                rotatingItemObj();
                break;
            case 5: 
                throwItemObj();
                break;
            case 6:
                useFireExtinguisher();
                break;
            case 7:
                goToPlaceObj();
                break;

        }

    }

    void walkObj()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical")!= 0)
        {
            index++;
        }
    }

    void jumpObj()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            index++;
        }
    }
    void crouchObj()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            index++;
        }
    }

    void pickUpObj()
    {
        if(physicsPickup.currentObject != null)
        {
            index++;
        }
    }

    void rotatingItemObj()
    {
        if (physicsPickup.GetRotationState())
        {
            index++;
        }
    }

    void throwItemObj()
    {
        if(physicsPickup.currentObject != null && Input.GetMouseButtonDown(0))
        {
            index++;
        }
    }

    void useFireExtinguisher()
    {
        if(inventoryManager.GetFireExtinguisherHoldState() && Input.GetMouseButtonDown(0))
        {
            index++;
        }
    }
    int placeIndex = 0;
    void goToPlaceObj()
    {
        collisionCheckForObjective.checkPlace(placesToGo[placeIndex]);
        if (collisionCheckForObjective.GetReachPlaceStatus())
        {
            if (placeIndex <= placesToGo.Length)
            {
                placeIndex++;
            }
            index++;
        }

    }





    public string GetCurrentObjective()
    {
        return Objectives[index];
    }
}
