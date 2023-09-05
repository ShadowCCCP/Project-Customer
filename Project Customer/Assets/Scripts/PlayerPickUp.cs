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

    private GameObject pickUp;

    private bool pickedUp = false;

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
        if (pickedUp)
        {
            pickUp.gameObject.transform.position = pickUpPos.position; 
            pickUp.gameObject.transform.rotation = pickUpPos.rotation;
        }
    }
    
    private void FixedUpdate()
    {
        Debug.DrawRay(pickUpPos.position, pickUpPos.forward, Color.red);
        if (Physics.Raycast(pickUpPos.position, Vector3.forward, out hit))
        {
           
            if(hit.collider.tag == "PickUp")
            {
                Debug.Log("raycast work");
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            if (Input.GetKeyDown(KeyCode.E) && !pickedUp)
            {
                Debug.Log("picked up");
                pickedUp=true;
                collision.gameObject.transform.parent = this.transform;
                
                pickUp = collision.gameObject;
            }
        }
    }

}
