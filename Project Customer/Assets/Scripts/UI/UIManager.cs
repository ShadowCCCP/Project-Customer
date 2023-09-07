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
    public TextMeshProUGUI LifeText;

    
    

    [Serializable]
    public struct ObjectNamesAndDescriptions
    {
         public string Name;
         public string Description;
    }

    public ObjectNamesAndDescriptions[] Objects;


    private PhysicsPickup playerPhysicsPickup;
    private Life playerLife;

    public float Oxygen = 100;
    public float OxygenRundownSpeed = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        ItemDescription.text = null;
        
        playerPhysicsPickup = FindObjectOfType<PhysicsPickup>();
        playerLife = FindObjectOfType<Life>();
        ItemName.text = playerPhysicsPickup.objectName;
        LifeText.text = "Life: " + playerLife.GetLife().ToString();



        OxygenLeftText.text = "Oxygen: " + Oxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemName.text != playerPhysicsPickup.objectName)
        {
            ItemName.text = playerPhysicsPickup.objectName;
            descriptionCheck();
        }

        if(LifeText.text != playerLife.GetLife().ToString())
        {
            LifeText.text = "Life: " + playerLife.GetLife().ToString();
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
