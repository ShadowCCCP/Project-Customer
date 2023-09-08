using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    Transform playerModel;

    [SerializeField]
    Transform _camera;

    [SerializeField]
    Transform cameraHolder;

    Animator animCam;
    Animator animModel;

    RotateCamera rotateCameraScript;
    PlayerMovement playerMovementScript;
    PhysicsPickup physicsPickupScript;

    void Start()
    {
        animCam = cameraHolder.GetComponent<Animator>();
        animModel = playerModel.GetComponent<Animator>();
        rotateCameraScript = _camera.GetComponent<RotateCamera>();
        playerMovementScript = playerModel.GetComponent<PlayerMovement>();
        physicsPickupScript = playerModel.GetComponent<PhysicsPickup>();
        Life.onDeath += Die;
    }

    void OnDestroy()
    {
        Life.onDeath -= Die;
    }

    private void Die()
    {
        animCam.SetTrigger("die");
        animModel.SetTrigger("die");
        rotateCameraScript.enabled = false;
        playerMovementScript.enabled = false;
        physicsPickupScript.enabled = false;
    }
}
