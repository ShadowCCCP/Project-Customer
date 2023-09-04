using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    Transform orientation;

    [SerializeField]
    float rotationSpeed = 5;

    Quaternion cameraRotation;
    float maxRotation = 90;

    void Start()
    {
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        RotateView();
    }

    private void RotateView()
    {
        cameraRotation.x += -Input.GetAxis("Mouse Y") * rotationSpeed;
        cameraRotation.y += Input.GetAxis("Mouse X") * rotationSpeed;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -maxRotation, maxRotation);

        transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);
        orientation.transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
