using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    UIManager uiManager;
    Animator anim;

    PhysicsPickup physicsPickup;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        anim = uiManager.GetComponent<Animator>();
        physicsPickup = FindObjectOfType<PhysicsPickup>();
        
        OnClickItems.onBedClicked += GoToSleep;
    }

    void OnDestroy()
    {
        OnClickItems.onBedClicked -= GoToSleep;
    }

    private void GoToSleep()
    {
        physicsPickup.activatePickupKeys = true;
        anim.SetTrigger("Sleep");
    }
}
