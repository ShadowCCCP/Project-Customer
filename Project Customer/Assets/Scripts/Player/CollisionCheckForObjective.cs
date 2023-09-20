using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckForObjective : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject placeToGo = null;
    bool reachedPlace;
    Dialogue dialogue;
    void Start()
    {
        dialogue = FindObjectOfType<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkPlace(GameObject place)
    {
        if (place != placeToGo)
        {
            reachedPlace = false;
            placeToGo = place;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (placeToGo != null)
        {
            if (other.gameObject == placeToGo)
            {
                reachedPlace = true;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dialogue != null)
        {
            if (other.tag == "solid")
            {
                dialogue.dialogueTriggerCollision(0);
            }
            if (other.tag == "electric")
            {
                dialogue.dialogueTriggerCollision(1);
            }
            if (other.tag == "cooking")
            {
                dialogue.dialogueTriggerCollision(2);
            }
        }
    }

    public bool GetReachPlaceStatus()
    {
        return reachedPlace;
    }
}
