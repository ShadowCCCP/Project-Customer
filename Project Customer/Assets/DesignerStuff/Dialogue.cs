using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
   
    private string opening = "Another fine day of brewing over. But goodness am I tuckered out now, I might have gone overboard at my old age, I should start preparing for bed.";
    private string tutorialDone = "The house really is a mess, I should clean up and do the final preparations before I go to bed!";
    private string bedTime = "Phew, that really took it out of me. I think it’s time for a well-deserved nap!";

    private List<String> allTexts = new List<String>(); 
    static private int textNumber;

    [SerializeField] GameObject textManager;
    private TextMeshProUGUI dialogue;

    void Start()
    {
        allTexts.Add(opening);
        allTexts.Add(tutorialDone);
        allTexts.Add(bedTime);
        //------------
        
        dialogue = textManager.GetComponent<TextMeshProUGUI>();
        
        // appear();
    }

    void OnTriggerEnter(Collider other)
    {
        print("Something collided");
        if(other.name == "PlayerObject")
        {
            appear();
        }
    }

    void appear()
    {        
        if(textNumber < allTexts.Count)
        {
            print("Come here");
            dialogue.text = allTexts[textNumber];
            textManager.SetActive(true);
        }

        Invoke("disappear", 5f);
    }

    void disappear()
    {
        print("go away");
        textManager.SetActive(false);
        textNumber += 1;
        gameObject.SetActive(false);
    }
}
