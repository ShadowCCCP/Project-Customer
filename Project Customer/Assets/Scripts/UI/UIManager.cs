using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI OxygenLeftText;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI LookedAtItem;
    public TextMeshProUGUI LookedAtItemDesc;
    public TextMeshProUGUI ExtraHint;


    [Serializable]
    public struct ObjectNamesAndDescriptions
    {
        public string Name;
        public string Description;
        public string LookAtDescription;
    }

    public ObjectNamesAndDescriptions[] Objects;


    private PhysicsPickup playerPhysicsPickup;
    private Life playerLife;

    public float Oxygen = 100;
    public float OxygenRundownSpeed = 0.5f;

   // [SerializeField]
    Camera _camera;

    [SerializeField]
    int lookAtDistance = 10;
    [SerializeField]
    LayerMask pickupMask;
    [SerializeField]
    LayerMask lookAtMask;


    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<Camera>();

        ItemDescription.text = null;
        LookedAtItem.text = null;
        LookedAtItemDesc.text = null;
        ExtraHint.text = null;

        playerPhysicsPickup = FindObjectOfType<PhysicsPickup>();
        playerLife = FindObjectOfType<Life>();
        //ItemName.text = playerPhysicsPickup.objectName;
        LifeText.text = "Life: " + playerLife.GetLife().ToString();



        OxygenLeftText.text = "Oxygen: " + Oxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemName.text == null || ItemName.text == "")
        {
            lookAtObject();
        }
        else
        {
            LookedAtItem.text = null;
            LookedAtItemDesc.text = null;
        }

        if (ItemName.text != playerPhysicsPickup.objectName)
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

    string findLookAtDesc(string objectToLookFor)
    {
        foreach (var o in Objects)
        {
            if (o.Name == objectToLookFor.ToString())
            {
                return o.LookAtDescription;
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

    private void lookAtObject()
    {
        Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, lookAtDistance, pickupMask)|| Physics.Raycast(cameraRay, out hitInfo, lookAtDistance, lookAtMask))
        {
            LookedAtItem.text = hitInfo.transform.name;
            LookedAtItemDesc.text = findLookAtDesc(hitInfo.transform.name);
        }
        else
        {
            LookedAtItem.text = null;
            LookedAtItemDesc.text = null;
        }
    }
}
