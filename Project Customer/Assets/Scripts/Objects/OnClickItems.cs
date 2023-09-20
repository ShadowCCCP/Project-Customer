using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickItems : MonoBehaviour
{
    public static event Action onStoveClicked;
    public static event Action onBedClicked;
    public static event Action onBathDoorClicked;
    public static event Action onBedDoorClicked;
    public static event Action onCandleClicked;

    public bool activateBed = true;

    //DO NOT ADD THIS TO PICKEABLEUP OBJECTS
    [SerializeField]
    GameObject afterClickObject;
    [SerializeField]
    GameObject beforeClickObject;

    Animator anim;

    [SerializeField]
    bool canBeClickedAgain = true;

    bool clicked = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Cliked()
    {
        PlayAnimation();
        SwitchLayerAfterClick();
        ReplaceObject();

        // If it's a door...
        DoorSound();

        // If it's a bed...
        BedSleep();

        // If it's a stove...
        StoveSound();

        // If it's a candle...
        CandleSound();
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

    private void SwitchLayerAfterClick()
    {
        if(!canBeClickedAgain)
        {
            gameObject.layer = 0;
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

    private void DoorSound()
    {
        if(onBathDoorClicked != null || onBedDoorClicked != null)
        {
            if (gameObject.tag == "BathDoor") onBathDoorClicked();
            else if (gameObject.tag == "BedDoor") onBedDoorClicked();
        }
    }

    private void CandleSound()
    {
        if(gameObject.tag == "Candle" && onCandleClicked != null)
        {
            onCandleClicked();
        }
    }

    public bool GetClickStatus()
    {
        return clicked;
    }



}
