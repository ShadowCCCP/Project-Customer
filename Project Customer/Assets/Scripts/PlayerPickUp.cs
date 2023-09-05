using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject pickUpHolder;
    [SerializeField]
    private Transform pickUpPos;
    [SerializeField]
    private Transform mainCameraPos;
    public int distanceMultiplierRaycast = 5;

    private GameObject pickUp;

    private bool pickedUp = false;
    private string objectName;

    private RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&& pickUp != null)
        {
            pickUp.gameObject.transform.parent = pickUpHolder.transform;
            pickedUp = false;
        }
        if (pickedUp) //update the pos of the picked up item
        {
            pickUp.gameObject.transform.position = pickUpPos.position; 
            pickUp.gameObject.transform.rotation = pickUpPos.rotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left-click. Do interaction");
        }
    }
    
    private void FixedUpdate()
    {
        Debug.DrawRay(mainCameraPos.position, mainCameraPos.forward * distanceMultiplierRaycast, Color.red);
        if (Physics.Raycast(mainCameraPos.position, mainCameraPos.forward *distanceMultiplierRaycast, out hit))
        {
            if(hit.collider.tag == "PickUp")
            {
                Debug.Log("raycast work");
                if (Input.GetKeyDown(KeyCode.E) && !pickedUp)
                {
                    pickedUp = true;
                    objectName = hit.collider.gameObject.name;
                    Debug.Log("picked up " + objectName);
                    hit.collider.gameObject.transform.parent = this.transform;

                    pickUp = hit.collider.gameObject;
                }
            }
        }
    }

}
