using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DesignerTutorial : MonoBehaviour
{

    [SerializeField] GameObject objectiveParent;
    [SerializeField] private Animator showUI;

    void OnTriggerEnter (Collider other)
    {
        if(other.name == "PlayerObject" && showUI != null)
        {
            objectiveParent.SetActive(true);
            showUI.SetTrigger("PlayAnimation");
        }

        this.enabled = false;
    }
}
