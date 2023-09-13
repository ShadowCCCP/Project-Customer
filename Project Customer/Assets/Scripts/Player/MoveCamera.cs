using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    Transform cameraPos;
    AudioManager audioManager;

    bool inCrouch = false;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        transform.position = cameraPos.position;
    }

    public void ActivateCrouch()
    {
        audioManager.Play("CrouchDown");
        cameraPos.localPosition = new Vector3(0, -0.5f, 0);
        inCrouch = true;
    }

    public void DeactivateCrouch()
    {
        audioManager.Play("CrouchUp");
        cameraPos.localPosition = new Vector3(0, 0.5f, 0);
        inCrouch = false;
    }

    public bool IsCrouching()
    {
        return inCrouch;
    }
}
