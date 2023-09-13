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

    [SerializeField]
    bool canBeClickedAgain = false;

    bool clicked = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cliked()
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
            {
                afterClickObject.SetActive(false);
                if (beforeClickObject)
                {
                    beforeClickObject.SetActive(true);
                }
            }
        }
    }
}
