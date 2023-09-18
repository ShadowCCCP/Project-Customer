using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsOpening : MonoBehaviour
{
    [SerializeField] GameObject doorOne;
    [SerializeField] GameObject doorTwo;

    private Animator doorA;
    private Animator doorB;

    private OnClickItems ociOne;
    private OnClickItems ociTwo;

    void Start() {
        doorA = doorOne.GetComponent<Animator>();
        doorB = doorTwo.GetComponent<Animator>();

        ociOne = doorOne.GetComponent<OnClickItems>();
        ociTwo = doorTwo.GetComponent<OnClickItems>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Book")
        {
            doorA.SetTrigger("PlayAnimation");
            doorB.SetTrigger("PlayAnimation");

            ociOne.enabled = true;
            ociTwo.enabled = true;

            this.enabled = false;
        } 
    }
}


