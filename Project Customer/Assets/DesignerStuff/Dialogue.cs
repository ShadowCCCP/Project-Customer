using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
   
    private string opening = "I have been wizarding for hours! Time to relax.";
    private TextMeshProUGUI dialogue;

    void Start()
    {
        dialogue = gameObject.GetComponent<TextMeshProUGUI>();
        
        // dialogue.text = "I have been wizarding for hours! Time to relax.";
        
        gameObject.SetActive(true);
    }
}
