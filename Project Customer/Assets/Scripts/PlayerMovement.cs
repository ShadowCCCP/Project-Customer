using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 1;

    [SerializeField]
    float jumpPower = 10;

    [SerializeField]
    float groundDrag = 1;

    [SerializeField]
    float inAirMultiplier = 0.4f;

    [SerializeField]
    Transform orientation;

    Rigidbody rb;

    float groundCheckDist = 1.1f;
    float horizontalInput;
    float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckInput();
        ApplyDrag();
        CapSpeed();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (IsGrounded())
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }
        else rb.AddForce(moveDirection.normalized * movementSpeed * 10f * inAirMultiplier, ForceMode.Force);
    }

    private void CheckInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    private void ApplyDrag()
    {
        if (IsGrounded()) rb.drag = groundDrag;
        else rb.drag = 0;
    }

    private void CapSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 cappedVelocity = flatVelocity.normalized * movementSpeed;
            rb.velocity = new Vector3(cappedVelocity.x, rb.velocity.y, cappedVelocity.z);
        }
    }

    private void Jump()
    {
        // To make the body jump the same height always
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        //Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * groundCheckDist, Color.cyan);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, groundCheckDist))
        {
            return true;
        }

        return false;
    }

    public bool IsMoving()
    {
        if (!(Mathf.Approximately(horizontalInput, 0f) && Mathf.Approximately(verticalInput, 0f)) && IsGrounded())
        {
            return true;
        }
        return false;
    }
}
