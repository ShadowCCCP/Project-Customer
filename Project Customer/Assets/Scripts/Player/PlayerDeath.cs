using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
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
        Life.onDeath += Die;
    }

    void OnDestroy()
    {
        Life.onDeath -= Die;
    }

    private void FixedUpdate()
    {
        transform.localPosition = playerModel.position;
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        rotateCameraScript.enabled = false;
        playerMovementScript.enabled = false;
        physicsPickupScript.enabled = false;
    }
}
