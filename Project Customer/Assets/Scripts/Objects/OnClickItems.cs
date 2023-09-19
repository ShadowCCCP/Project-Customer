using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickItems : MonoBehaviour
{
    public static event Action onStoveClicked;
    public static event Action onBedClicked;

    public bool activateBed = true;

    //DO NOT ADD THIS TO PICKEABLEUP OBJECTS
    [SerializeField]
    GameObject afterClickObject;
    [SerializeField]
    GameObject beforeClickObject;

    Animator anim;

    [SerializeField]
    bool canBeClickedAgain = false;

    bool clicked = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Cliked()
    {
        // If it's a bed...
        BedSleep();

        // If it's a stove...
        StoveSound();

        ReplaceObject();
        PlayAnimation();
    }

    private void ReplaceObject()
    {

        if (clicked == false)
        {
                clicked = true;
                if (afterClickObject)
                {
                    afterClickObject.SetActive(true);
                    if (beforeClickObject)
                    {
                        beforeClickObject.SetActive(false);
                    }
                }
        }
        
        else if (canBeClickedAgain)
        {
            clicked = false;
            if (afterClickObject)
                clicked = false;
                if (afterClickObject)
                {
                    afterClickObject.SetActive(false);
                    if (beforeClickObject)
                    {
                        beforeClickObject.SetActive(true);
                    }
                }
        }
    }

    private void PlayAnimation()
    {
        if (anim != null)
        {
            anim.SetTrigger("PlayAnimation");
        }
    }

    private void BedSleep()
    {
        if(gameObject.tag == "Bed" && onBedClicked != null && activateBed)
        {
            activateBed = false;
            onBedClicked();
        }
    }

    private void StoveSound()
    {
        if(gameObject.tag == "Stove" && onStoveClicked != null)
        {
            onStoveClicked();
        }
    }

    public bool GetClickStatus()
    {
        return clicked;
    }



}
