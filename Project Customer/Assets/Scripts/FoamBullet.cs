using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoamBullet : MonoBehaviour
{
    [SerializeField]
    float foamRemoveTimer = 3; 

    Rigidbody rb;
    bool hitOnce;

    Collider otherFoam;
    bool hitFoam;

    float groundCheckDist = 0.15f;

    Coroutine myCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CapFallingVelocity();

        if(hitFoam && otherFoam == null)
        {
            rb.isKinematic = false;
            hitOnce = false;
        }
    }

    public void Shoot(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    private void CapFallingVelocity()
    {
        if(rb.velocity.y < -1)
        {
            rb.velocity = new Vector3(rb.velocity.x, -1, rb.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitOnce && HitBottom())
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            hitOnce = true;

            if(other.tag == "FoamBullet")
            {
                otherFoam = other;
                hitFoam = true;
            }
            else hitFoam = false;

            Transform newParent = other.transform;
            while(newParent.parent != null && newParent.parent.transform.tag == "FoamBullet")
            {
                newParent = newParent.parent;
            }
            transform.SetParent(newParent);

            if(myCoroutine != null)
            {
                StopCoroutine(myCoroutine);
            }
            myCoroutine = StartCoroutine(RemoveFoamTimer());
        }
    }

    private IEnumerator RemoveFoamTimer()
    {
        yield return new WaitForSeconds(foamRemoveTimer);

        if (gameObject.transform.childCount > 0)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.SetParent(null);
            }
        }

        Destroy(gameObject);
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
}
