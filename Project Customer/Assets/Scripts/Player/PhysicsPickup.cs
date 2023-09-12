using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{
    [SerializeField]
    LayerMask pickupMask;

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
    public string objectName;

    float objectNormalAngularDrag;

    // Mouse rotation control
    [SerializeField]
    float rotationSpeed = 2;

    bool isRotating = false;
    RotateCamera rotateCameraScript;

    bool itemThrown; //for objecctive
    void Start()
    {
        rotateCameraScript = _camera.GetComponent<RotateCamera>();
    }

    void Update()
    {
        PickupItem();
        RotateItem();
        ThrowItem();
    }

    void FixedUpdate()
    {
        if(currentObject != null)
        {
            // Move currentObject to the pickupTarget...
            Vector3 directionToPoint = pickupTarget.position - currentObject.position;
            float distanceToPoint = directionToPoint.magnitude;

            currentObject.velocity = directionToPoint * pickupMoveSpeed * distanceToPoint;
            currentObject.angularDrag = angularDrag;
        }
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
                currentObject = hitInfo.rigidbody;
                objectNormalAngularDrag = currentObject.angularDrag;
                objectName = currentObject.name;
                currentObject.useGravity = false;
            }
        }
    }

    private void RotateItem()
    {
        if (currentObject != null && currentObject.tag != "Bucket")
        {
            if (Input.GetMouseButtonDown(1))
            {
                rotateCameraScript.enabled = false;
                isRotating = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                rotateCameraScript.enabled = true;
                isRotating = false;
            }

            if (isRotating)
            {
                float xRotation = Input.GetAxisRaw("Mouse X") * rotationSpeed;
                float yRotation = Input.GetAxisRaw("Mouse Y") * rotationSpeed;

                currentObject.transform.Rotate(Vector3.down, xRotation);
                currentObject.transform.Rotate(Vector3.right, yRotation);
            }
        }
    }



    private void ThrowItem()
    {
        if (currentObject != null && Input.GetMouseButtonDown(0))
        {
            itemThrown = true;
            Rigidbody rb = currentObject.GetComponent<Rigidbody>();
            DropObject();
            rb.AddForce(_camera.transform.forward * throwPower, ForceMode.Impulse);

        }
        else itemThrown = false;
    }

    private void DropObject()
    {
        currentObject.angularDrag = objectNormalAngularDrag;
        currentObject.useGravity = true;
        currentObject = null;
        objectName = null;
    }

    public bool GetRotationState()
    {
        return isRotating;
    }

    public bool GetThrownStatus()
    {
        return itemThrown;
    }
}
