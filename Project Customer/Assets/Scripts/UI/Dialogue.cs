using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueBox;
    TextMeshProUGUI dialoguetext;
    [SerializeField]
    string dialogueBegginignOfGame;
    [SerializeField] 
    string dialogueTextWhenListHere;
    [SerializeField]
    string[] dialogueLinesAfterGroupOfQuests;
    [SerializeField]
    string spiritSolidText;
    [SerializeField]
    string spiritElectricText;
    [SerializeField]
    string spiritCookingText;
    int dialogueQuestsIndex = 0;
    bool dialogueActive = false;
    Animator anim;
    [SerializeField]
    float timeTextOnScreen = 2;
    float timeLeft;


    // Start is called before the first frame update
    void Start()
    {
        anim = dialogueBox.GetComponent<Animator>();
        dialoguetext = dialogueBox.GetComponent<TextMeshProUGUI>(); 
        dialoguetext.text = "";
        dialoguetext.text = dialogueBegginignOfGame.ToString();
        dialogueNew();
    }

    // Update is called once per frame
    void Update()
    {
        timer();
    }
    public void dialogueWhenListHere()
    {
        if (!dialogueActive)
        {
            dialoguetext.text = dialogueTextWhenListHere.ToString();
            dialogueNew();
        }
    }
    public void dialogueTriggerQuests()
    {

        if (!dialogueActive )
        {
            dialoguetext.text = dialogueLinesAfterGroupOfQuests[dialogueQuestsIndex].ToString();
            dialogueNew();
        }
        if(dialogueQuestsIndex  < dialogueLinesAfterGroupOfQuests.Length-1)
        {
            dialogueQuestsIndex++;
        }
    }

    public void dialogueTriggerCollision(int spiritType) //0 = solid; 1= electrc ; 1 = cooking
    {
        if (!dialogueActive) 
        {
            switch (spiritType)
            {
                case 0:
                    dialoguetext.text = spiritSolidText.ToString();
                    dialogueNew();
                    break;
                case 1:
                    dialoguetext.text = spiritElectricText.ToString();
                    dialogueNew();
                    break;
                case 2:
                    dialoguetext.text = spiritCookingText.ToString();
                    dialogueNew();
                    break;
            }
        }
    }

    void timer()
    {
        if (dialogueActive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                dialogueActive= false;
                anim.SetTrigger("PlayAnimation");
            }
        }
    }

    void dialogueNew()
    {
        anim.SetTrigger("PlayAnimation");
        dialogueActive = true;
        timeLeft = timeTextOnScreen;
    }

}
