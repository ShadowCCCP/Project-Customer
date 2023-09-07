using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI OxygenLeftText;

    [Serializable]
    public struct ObjectNamesAndDescriptions
    {
         public string Name;
         public string Description;
    }

    public ObjectNamesAndDescriptions[] Objects;


    private PhysicsPickup player;

    public float Oxygen = 100;
    public float OxygenRundownSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        ItemDescription.text = null;
        
        player = FindObjectOfType<PhysicsPickup>();
        ItemName.text = player.objectName;
        

        OxygenLeftText.text = "Oxygen: " + Oxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemName.text != player.objectName)
        {
            ItemName.text = player.objectName;
            descriptionCheck();
            //change desc
        }
        oxygenRundown();
    }

    void descriptionCheck()
    {
        if (ItemName.text != null)
        {
            ItemDescription.text = findDesc(ItemName.text);
        }
        else
        {
            ItemDescription.text = null;
        }
    }

    string findDesc(string objectToLookFor)
    {
        foreach(var o in Objects)
        {
            if(o.Name == objectToLookFor.ToString())
            {
                return o.Description;
            }
        }
        return null;
    }
    void oxygenRundown()
    {
        Oxygen -= Time.deltaTime * OxygenRundownSpeed;
        OxygenLeftText.text = "Oxygen: " + (int)Oxygen;
        if (Oxygen < 0)
        {
            //death
        }
    }
}
