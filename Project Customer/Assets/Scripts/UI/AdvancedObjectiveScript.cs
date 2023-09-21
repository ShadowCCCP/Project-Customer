using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class AdvancedObjectivesScript : MonoBehaviour
{

    [SerializeField]
    AdvancedObjectiveClass[] Objectives;

    PhysicsPickup physicsPickup;
    InventoryManager inventoryManager;
    CollisionCheckForObjective collisionCheckForObjective;
    Dialogue dialogue;
  

    int objectiveIndex = 0;
    int objectiveType;

    void Start()
    {
        physicsPickup = FindObjectOfType<PhysicsPickup>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        collisionCheckForObjective = FindObjectOfType<CollisionCheckForObjective>();
        dialogue = FindObjectOfType<Dialogue>();

    }

    // Update is called once per frame
    void Update()
    {
        if (objectiveIndex < Objectives.Length)
        {
            for (int i = 0; i < Objectives[objectiveIndex].ObjectiveNumber; i++)
            {
                checkObjType(objectiveIndex + i);
                objectiveTypeCheck(objectiveIndex + i);
            }
            checkIfAllObjDone();
        }

    }

    void objectiveTypeCheck(int i)
    {
        switch (objectiveType)
        {
            case 0:
                walkObj(i);
                break;
            case 1:
                jumpObj(i);
                break;
            case 2:
                crouchObj(i);
                break;
            case 3:
                pickUpObj(i);
                break;
            case 4:
                rotatingItemObj(i);
                break;
            case 5:
                throwItemObj(i);
                break;
            case 6:
                useFireExtinguisher(i);
                break;
            case 7:
                goToPlaceObj(i);
                break;
            case 8:
                pickUpSpecificItemObj(i);
                break;
            case 9:
                putItemInPlace(i);
                break;
            case 10:
                onClickObj(i);
                break;

        }
    }
    void checkObjType(int index)
    {
        if (index < Objectives.Length)
        {
            if (Objectives[index].TaskUnderstoodByCode.Contains("Walk") || Objectives[index].TaskUnderstoodByCode.Contains("walk"))
            {
                objectiveType = 0;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("Jump") || Objectives[index].TaskUnderstoodByCode.Contains("jump"))
            {
                objectiveType = 1;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("Crouch") || Objectives[index].TaskUnderstoodByCode.Contains("crouch"))
            {
                objectiveType = 2;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("Pick up") || Objectives[index].TaskUnderstoodByCode.Contains("pick up"))
            {
                objectiveType = 3;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("rotate") || Objectives[index].TaskUnderstoodByCode.Contains("Rotate"))
            {
                objectiveType = 4;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("Throw") || Objectives[index].TaskUnderstoodByCode.Contains("throw"))
            {
                objectiveType = 5;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("extinguish") || Objectives[index].TaskUnderstoodByCode.Contains("Extinguish"))
            {
                objectiveType = 6;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("Go to") || Objectives[index].TaskUnderstoodByCode.Contains("go to"))
            {
                objectiveType = 7;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("Pick up the") || Objectives[index].TaskUnderstoodByCode.Contains("pick up the"))
            {
                objectiveType = 8;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("Put the") || Objectives[index].TaskUnderstoodByCode.Contains("put the"))
            {
                objectiveType = 9;
            }
            if (Objectives[index].TaskUnderstoodByCode.Contains("Click") || Objectives[index].TaskUnderstoodByCode.Contains("click"))
            {
                objectiveType = 10;
            }
        }
    }

    

    void walkObj(int i)
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical")!= 0)
        {
            Objectives[i].done = true;
        }
    }

    void jumpObj(int i)
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Objectives[i].done = true;
        }
    }
    void crouchObj(int i)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Objectives[i].done = true;
        }
    }

    void pickUpObj(int i)
    {
        if(physicsPickup.currentObject != null)
        {
            Objectives[i].done = true;
        }
    }

    void rotatingItemObj(int i)
    {
        if (physicsPickup.GetRotationState())
        {
            Objectives[i].done = true;
        }
    }

    void throwItemObj(int i)
    {
        if(physicsPickup.GetThrownStatus())
        {
            Objectives[i].done = true;
        }

    }

    void useFireExtinguisher(int i)
    {
        if(inventoryManager.GetFireExtinguisherHoldState() && Input.GetMouseButtonDown(0))
        {
            Objectives[i].done = true;
        }
    }
    void goToPlaceObj(int i)
    {
        collisionCheckForObjective.checkPlace(Objectives[i].GameObject);
        if (collisionCheckForObjective.GetReachPlaceStatus())
        {
            Objectives[i].done = true;
        }

    }

    void pickUpSpecificItemObj(int i)
    {
        if (physicsPickup.currentObject)
        {
            if (physicsPickup.currentObject.name == Objectives[i].GameObject.name)
            {
                Objectives[i].done = true;
            }
        }
    }

    void putItemInPlace(int i)
    {
        SpecificCollisions specificCollisions = Objectives[i].GameObject.GetComponent<SpecificCollisions>();
        if (specificCollisions.GetCollisionStatus())
        {
            Objectives[i].done = true;
        }
    }

    void onClickObj(int i)
    {
        OnClickItems onClickItem = Objectives[i].GameObject.GetComponent<OnClickItems>();

        if (onClickItem.GetClickStatus())
        {

             Objectives[i].done = true;

        }
        
    }

    int groupsOfQuestsDone = 0;
    [SerializeField]
    Transform bed;
    OnClickItems onClickItems;
    void checkIfAllObjDone()
    {
        bool check = true;
        for(int i = 0; i< Objectives[objectiveIndex].ObjectiveNumber; i++)
        {
            if (!Objectives[i + objectiveIndex].done)
            {
                check = false;
            }
        }
        if(check)
        {
            //group of objectives done 
            dialogue.dialogueTriggerQuests();
            objectiveIndex += Objectives[objectiveIndex].ObjectiveNumber;
            groupsOfQuestsDone++;
        }
        if(groupsOfQuestsDone == 2)
        {
            onClickItems = bed.GetComponent<OnClickItems>();
            onClickItems.activateBed = true;
        }
    }


    public string GetCurrentObjective(int textNumber)
    {
        if (objectiveIndex+textNumber-1 < Objectives.Length)
        {
            if (textNumber - 1 < Objectives[objectiveIndex].ObjectiveNumber && !Objectives[objectiveIndex + textNumber - 1].done)
            {
                return Objectives[objectiveIndex + textNumber - 1].TaskThePlayerSees;
            }
        }

        return " ";
       
    }


}
