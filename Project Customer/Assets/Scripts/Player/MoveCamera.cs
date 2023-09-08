using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    Transform cameraPos;

    bool inCrouch = false;

    void Update()
    {
        transform.position = cameraPos.position;
    }

    public void ActivateCrouch()
    {
        cameraPos.localPosition = new Vector3(0, 0.5f, 0);
        inCrouch = true;
    }

    public void DeactivateCrouch()
    {
        cameraPos.localPosition = new Vector3(0, 0.5f, 0);
        inCrouch = false;
    }

    public bool IsCrouching()
    {
        return inCrouch;
    }
}
