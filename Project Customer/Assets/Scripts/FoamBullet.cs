using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoamBullet : MonoBehaviour
{
    [SerializeField]
    float foamRemoveTimer = 20; 

    Rigidbody rb;
    bool hitOnce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitOnce)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            hitOnce = true;
            
        }
    }
}
