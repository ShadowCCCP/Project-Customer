using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingSoda : MonoBehaviour
{
    [SerializeField]
    float repellMultiplier = 4;

    PlayerMovement playerPos;
    Rigidbody rb;

    void Start()
    {
        playerPos = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        Fire.onRepellSoda += RepellSoda;
    }

    void OnDestroy()
    {
        Fire.onRepellSoda -= RepellSoda;
    }

    private void RepellSoda()
    {
        rb.AddForce(playerPos.transform.position - transform.position * repellMultiplier, ForceMode.Impulse);
    }
}
