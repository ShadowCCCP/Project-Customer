using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    UIManager uiManager;
    Animator anim;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        anim = uiManager.GetComponent<Animator>();
        
        OnClickItems.onBedClicked += GoToSleep;
    }

    void OnDestroy()
    {
        OnClickItems.onBedClicked -= GoToSleep;
    }

    private void GoToSleep()
    {
        anim.SetTrigger("Sleep");
    }
}
