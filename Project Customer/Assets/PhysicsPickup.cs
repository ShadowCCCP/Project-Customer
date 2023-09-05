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

    Rigidbody currentObject;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // If you hold an object, let go of it...
            if(currentObject != null)
            {
                currentObject.useGravity = true;
                currentObject = null;
                return;
            }

            // If you're looking at a pickable item, pick it up...
            Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if(Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange, pickupMask))
            {
                currentObject = hitInfo.rigidbody;
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
        }
    }
}
