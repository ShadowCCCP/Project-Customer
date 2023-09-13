using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoamBullet : MonoBehaviour
{
    [SerializeField]
    float foamRemoveTimer = 1; 

    bool hitOnce;
    float groundCheckDist = 0.18f;
    Rigidbody rb;

    Collider otherFoam;
    bool hitFoam;
    Coroutine myCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ReactivatePhysics();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hitOnce && HitBottom())
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            if (collision.collider.tag == "FoamBullet")
            {
                otherFoam = collision.collider;
                hitFoam = true;
            }
            else hitFoam = false;

            if (myCoroutine != null)
            {
                StopCoroutine(myCoroutine);
            }
            myCoroutine = StartCoroutine(RemoveFoamTimer());
            hitOnce = true;
        }
    }

    private bool HitBottom()
    {
        //Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * groundCheckDist, Color.cyan);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, groundCheckDist))
        {
            return true;
        }

        return false;
    }

    private IEnumerator RemoveFoamTimer()
    {
        yield return new WaitForSeconds(foamRemoveTimer);
        Destroy(gameObject);
    }

    private void ReactivatePhysics()
    {
        if (hitFoam && otherFoam == null)
        {
            rb.isKinematic = false;
            hitOnce = false;
        }
    }
}
