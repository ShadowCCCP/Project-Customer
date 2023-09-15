using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickItems : MonoBehaviour
{
    // Start is called before the first frame update


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
            //Debug.Log("oooo");
            anim.SetTrigger("PlayAnimation");
        }
    }

}
