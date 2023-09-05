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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("picked up");
                pickedUp=true;
                collision.gameObject.transform.parent = this.transform;
                
                pickUp = collision.gameObject;
            }
        }
    }

}
