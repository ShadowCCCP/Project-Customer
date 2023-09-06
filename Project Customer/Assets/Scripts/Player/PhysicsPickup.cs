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

    public Rigidbody currentObject;
    public string objectName;

    private float objectNormalAngularDrag;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // If you hold an object, let go of it...
            if(currentObject != null)
            {
                currentObject.angularDrag = objectNormalAngularDrag;
                currentObject.useGravity = true;
                currentObject = null;
                objectName = null;
                return;
            }

            // If you're looking at a pickable item, pick it up...
            Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if(Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange, pickupMask))
            {
                objectNormalAngularDrag = currentObject.angularDrag;
                currentObject = hitInfo.rigidbody;
                objectName = currentObject.name;
                currentObject.useGravity = false;
            }
        }
    }

    void FixedUpdate()
    {
        if(currentObject != null)
        {
            // Move currentObject to the pickupTarget...
            Vector3 directionToPoint = pickupTarget.position - currentObject.position;
            float distanceToPoint = directionToPoint.magnitude;

            currentObject.velocity = directionToPoint * 12 * distanceToPoint;
            currentObject.angularDrag = angularDrag;
        }
    }
}
