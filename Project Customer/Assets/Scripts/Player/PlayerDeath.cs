using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    //Listen to event to activate this script...

    [SerializeField]
    Transform playerModel;

    [SerializeField]
    Transform _camera;

    Animator anim;

    RotateCamera rotateCameraScript;
    PlayerMovement playerMovementScript;
    PhysicsPickup physicsPickupScript;

    void Start()
    {
        anim = GetComponent<Animator>();
        rotateCameraScript = _camera.GetComponent<RotateCamera>();
        playerMovementScript = playerModel.GetComponent<PlayerMovement>();
        physicsPickupScript = playerModel.GetComponent<PhysicsPickup>();
        // Subscribe to event here...
        Die();
    }

    void OnDestroy()
    {
        // Unsubscribe from event here...
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        rotateCameraScript.enabled = false;
        playerMovementScript.enabled = false;
        physicsPickupScript.enabled = false;
    }
}
