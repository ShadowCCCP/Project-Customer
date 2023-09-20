using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{
    public static event Action onPickupFireExtinguisher;

    public bool activatePickupKeys = true;

    [SerializeField]
    LayerMask pickupMask;

    [SerializeField]
    LayerMask rotateMask;

    [SerializeField]
    Camera _camera;
    
    [SerializeField]
    Transform pickupTarget;

    [SerializeField]
    float pickupRange;

    [SerializeField]
    float angularDrag = 5;

    [SerializeField]
    float pickupMoveSpeed = 12;

    [SerializeField]
    float throwPower = 6;

    public Rigidbody currentObject;

    float objectNormalAngularDrag;


    // Mouse rotation control
    [SerializeField]
    float rotationSpeed = 2;

    bool isRotating = false;
    RotateCamera rotateCameraScript;

    bool itemThrown; //for objective
    bool rotationOnlyItem;

    AudioManager audioManager;
    string[] throwSounds = { "Throw1", "Throw2", "Throw3" };
    string pickupSound = "Pickup";

    void Start()
    {
        Fire.onRepellSoda += LetGoOffBakingSoda;
        audioManager = FindObjectOfType<AudioManager>();
        rotateCameraScript = _camera.GetComponent<RotateCamera>();
    }

    void Update()
    {
        PickupItem();
        RotateItem();
        ItemRotation();
        ThrowItem();

    }

    void FixedUpdate()
    {
        if(currentObject != null && !rotationOnlyItem)
        {
            // Move currentObject to the pickupTarget...
            Vector3 directionToPoint = pickupTarget.position - currentObject.position;
            float distanceToPoint = directionToPoint.magnitude;

            currentObject.velocity = directionToPoint * pickupMoveSpeed * distanceToPoint;
            currentObject.angularDrag = angularDrag;
        }
    }

    private void OnDestroy()
    {
        Fire.onRepellSoda -= LetGoOffBakingSoda;
    }

    private void PickupItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If you hold an object, let go of it...
            if (currentObject != null)
            {
                DropObject();
                
                return;
            }

            // If you're looking at a pickable item, pick it up...
            Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange, pickupMask))
            {
                if (hitInfo.transform.tag == "Key" && !activatePickupKeys) return;

                audioManager.Play(pickupSound);
                currentObject = hitInfo.rigidbody;
                objectNormalAngularDrag = currentObject.angularDrag;
                //objectName = currentObject.name;
                currentObject.useGravity = false;
                rotationOnlyItem = false;

                // If object is fire extinguisher, add to inventory...
                if (currentObject.GetComponent<FireExtinguisher>() != null && onPickupFireExtinguisher != null)
                {
                    onPickupFireExtinguisher();
                    Destroy(currentObject.gameObject);
                }

            }
 
            else if (Physics.Raycast(cameraRay, out RaycastHit hitInfoRotation, pickupRange, rotateMask))
            {
                currentObject = hitInfoRotation.rigidbody;
                isRotating = true;
                rotationOnlyItem = true;
            }

        }
    }

    private void RotateItem()
    {
        if (currentObject != null && currentObject.tag != "Bucket" && !rotationOnlyItem)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //rotateCameraScript.enabled = false;
                isRotating = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                //rotateCameraScript.enabled = true;
                rotateCameraScript.enabled = true;
                isRotating = false;
            }

            //ItemRotation();
        }
    }

    private void ItemRotation()
    {
        if (isRotating)
        {
            rotateCameraScript.enabled = false;
            float xRotation = Input.GetAxisRaw("Mouse X") * rotationSpeed;
            float yRotation = Input.GetAxisRaw("Mouse Y") * rotationSpeed;

            currentObject.transform.Rotate(Vector3.down, xRotation, Space.World);
            currentObject.transform.Rotate(Vector3.right, yRotation, Space.World);

            //currentObject.transform.eulerAngles += new Vector3(xRotation, yRotation, yRotation);
        }

    }

    private void LetGoOffBakingSoda()
    {
        if(currentObject != null)
        {
            DropObject();
        }
    }

    private void ThrowItem()
    {
        if (currentObject != null && Input.GetMouseButtonDown(0) && !rotationOnlyItem)
        {
            int randomNumber = UnityEngine.Random.Range(0, 3);
            audioManager.Play(throwSounds[randomNumber]);

            itemThrown = true;
            Rigidbody rb = currentObject.GetComponent<Rigidbody>();
            DropObject();
            rb.AddForce(_camera.transform.forward * throwPower, ForceMode.Impulse);
        }
        else itemThrown = false;
    }

    public void DropObject()
    {
        if (currentObject)
        {
            if (!rotationOnlyItem)
            {
                currentObject.angularDrag = objectNormalAngularDrag;
                currentObject.useGravity = true;
            }
            else
            {
                rotateCameraScript.enabled = true;
            }
        }

        currentObject = null;
        isRotating = false;
       // objectName = null;
    }

    public bool GetRotationState()
    {
        return isRotating;
    }

    public bool GetThrownStatus()
    {
        return itemThrown;
    }

    public float GetPickupDistance()
    {
        return pickupRange;
    }
}
