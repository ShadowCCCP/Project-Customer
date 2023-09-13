using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject objectToCollideWith;
    bool collsionHappened;

    [SerializeField]
    GameObject objectBeforeCollision;
    [SerializeField]
    GameObject objectAfterCollision;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == objectToCollideWith)
        {
            collsionHappened = true;
            if (objectAfterCollision != null)
            {
                objectAfterCollision.SetActive(true);
                if (objectBeforeCollision)
                {
                    objectBeforeCollision.SetActive(false);
                }
            }
        }
        else
        {
            collsionHappened = false;
        }
    }

    public bool GetCollisionStatus()
    {
        return collsionHappened;
    }
}
