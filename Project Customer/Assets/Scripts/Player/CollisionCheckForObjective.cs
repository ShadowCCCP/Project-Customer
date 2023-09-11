using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckForObjective : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject placeToGo = null;
    bool reachedPlace;
    void Start()
    {
        
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
    public bool GetReachPlaceStatus()
    {
        return reachedPlace;
    }
}
