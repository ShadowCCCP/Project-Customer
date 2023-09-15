using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ObjectivesScript : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    //  string[] ObjectivesOld;

    [SerializeField]
    ObjectiveClass[] Objectives;

    PhysicsPickup physicsPickup;
    InventoryManager inventoryManager;
    CollisionCheckForObjective collisionCheckForObjective;

    [SerializeField]
    GameObject[] placesToGo;

    [SerializeField]
    GameObject[] itemsToPickUp;

    [SerializeField]
    SpecificCollisions[] objToCollideWithForThePutItemInPlace;

    //walk 
    //jump
    //crouch
    //pick up object
    //hold right click to rotate obj
    //throw obj
    //use fire extinguisher

    //go to a place
    //pickup spec obj

    //capital sensitive

    int objectiveIndex = 0;
    int objectiveType;

    void Start()
    {
        physicsPickup = FindObjectOfType<PhysicsPickup>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        collisionCheckForObjective = FindObjectOfType<CollisionCheckForObjective>();

    }

    // Update is called once per frame
    void Update()
    {
        checkObjType();
        switch (objectiveType)
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
            case 8:
                pickUpSpecificItemObj();
                break;
            case 9:
                putItemInPlace();
                break;

        }

    }

    

    void checkObjType()
    {
        if (objectiveIndex < Objectives.Length)
        {
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Walk") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("walk"))
            {
                objectiveType = 0;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Jump") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("jump"))
            {
                objectiveType = 1;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Crouch") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("crouch"))
            {
                objectiveType = 2;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Pick up") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("pick up"))
            {
                objectiveType = 3;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("rotate") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Rotate"))
            {
                objectiveType = 4;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Throw") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("throw"))
            {
                objectiveType = 5;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("extinguish") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Extinguish"))
            {
                objectiveType = 6;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Go to") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("go to"))
            {
                objectiveType = 7;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Pick up the") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("pick up the"))
            {
                objectiveType = 8;
            }
            if (Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("Put the") || Objectives[objectiveIndex].TaskUnderstoodByCode.Contains("put the"))
            {
                objectiveType = 9;
            }
        }
    }

    

    void walkObj()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical")!= 0)
        {
            objectiveIndex++;
        }
    }

    void jumpObj()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            objectiveIndex++;
        }
    }
    void crouchObj()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            objectiveIndex++;
        }
    }

    void pickUpObj()
    {
        if(physicsPickup.currentObject != null)
        {
            objectiveIndex++;
        }
    }

    void rotatingItemObj()
    {
        if (physicsPickup.GetRotationState())
        {
            objectiveIndex++;
        }
    }

    void throwItemObj()
    {
        if(physicsPickup.GetThrownStatus())
        {
            objectiveIndex++;
        }

    }

    void useFireExtinguisher()
    {
        if(inventoryManager.GetFireExtinguisherHoldState() && Input.GetMouseButtonDown(0))
        {
            objectiveIndex++;
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
            objectiveIndex++;
        }

    }

    int itemToPickUpIndex = 0;

    void pickUpSpecificItemObj()
    {
        if (physicsPickup.currentObject)
        {
            if (physicsPickup.currentObject.name == itemsToPickUp[itemToPickUpIndex].name)
            {
                if (itemToPickUpIndex <= itemsToPickUp.Length)
                {
                    itemToPickUpIndex++;
                }
                objectiveIndex++;
            }
        }
    }


    int objToCollideWithIndex = 0;
    void putItemInPlace()
    {
        if (objToCollideWithIndex < objToCollideWithForThePutItemInPlace.Length)
        {
            if (objToCollideWithForThePutItemInPlace[objToCollideWithIndex].GetCollisionStatus())
            {
                objToCollideWithIndex++;
                objectiveIndex++;
            }
        }
    }




    public string GetCurrentObjective()
    {
        if (objectiveIndex < Objectives.Length)
        {
            return Objectives[objectiveIndex].TaskThePlayerSees;
           // return " ";
        }
        else
        {
            return " ";
        }
    }
}
