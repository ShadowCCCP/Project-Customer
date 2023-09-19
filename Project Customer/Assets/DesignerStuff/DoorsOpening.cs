using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsOpening : MonoBehaviour
{
    private Animator showUI;

    void Start() {
       
    }

    void OnTriggerEnter(Collider other)
    {
        showUI.SetTrigger("MoveObjective");
    }
}


