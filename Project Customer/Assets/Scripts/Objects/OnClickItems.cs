using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickItems : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject afterClickObject;
    [SerializeField]
    GameObject beforeClickObject;

    [SerializeField]
    bool canbeClickedAgain = false;

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
        else if (canbeClickedAgain)
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
