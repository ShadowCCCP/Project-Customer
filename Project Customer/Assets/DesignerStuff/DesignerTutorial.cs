using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DesignerTutorial : MonoBehaviour
{

    [SerializeField] GameObject objectiveParent;
    void OnTriggerExit (Collider other)
    {
        print(other.name);
        if(other.name == "PlayerObject")
        {
            objectiveParent.SetActive(true);
        }

        this.enabled = false;
    }
}
