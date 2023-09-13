using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI OxygenLeftText;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI LookedAtItem;
    public TextMeshProUGUI LookedAtItemDesc;
    public TextMeshProUGUI ExtraHint;
    public TextMeshProUGUI Objective;

    PhysicsPickup pPickup;


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
    private ObjectivesScript objectivesScript;

    Camera _camera;

    [SerializeField]
    LayerMask pickupMask;
    [SerializeField]
    LayerMask lookAtMask;

    [SerializeField]
    bool useOutline = false;

    [SerializeField]
    Material shaderMaterial;
    [SerializeField]
    Material shaderMaterialEmpty;

    Renderer rend;

    [SerializeField]
    Slider sliderOxygen;
    [SerializeField]
    Slider sliderHealth;


    // Start is called before the first frame update
    void Start()
    {
        pPickup = FindObjectOfType<PhysicsPickup>();

        _camera = FindObjectOfType<Camera>();
        objectivesScript = FindObjectOfType<ObjectivesScript>();

        ItemDescription.text = null;
        LookedAtItem.text = null;
        LookedAtItemDesc.text = null;
        ExtraHint.text = null;

        playerPhysicsPickup = FindObjectOfType<PhysicsPickup>();
        playerLife = FindObjectOfType<Life>();
        //ItemName.text = playerPhysicsPickup.objectName;

        LifeText.text = "Life: " + playerLife.GetLife().ToString();
        OxygenLeftText.text = "Oxygen: " + playerLife.GetOxygen().ToString(); ;


     
       // shader = Material.Find("OutlineShaderMaterial");

    }

    // Update is called once per frame
    void Update()
    {
        SetSliderValues();
        if(ItemName.text == null || ItemName.text == "")
        {
            lookAtObject();
        }
        else
        {
            LookedAtItem.text = null;
            LookedAtItemDesc.text = null;
        }

        if (playerPhysicsPickup.currentObject != null)
        {
            if (ItemName.text != playerPhysicsPickup.currentObject.name)
            {
                ItemName.text = playerPhysicsPickup.currentObject.name;
                descriptionCheck();
            }
        }
        else
        {
            ItemName.text = null;
            ItemDescription.text = null;
        }

        if(LifeText.text != playerLife.GetLife().ToString())
        {
            LifeText.text = "Life: " + playerLife.GetLife().ToString();
        }
        if (OxygenLeftText.text != playerLife.GetOxygen().ToString())
        {
            OxygenLeftText.text = "Oxygen: " + playerLife.GetOxygen().ToString();
        }

        if(Objective.text != objectivesScript.GetCurrentObjective().ToString())
        {
            Objective.text = "Objective: " + objectivesScript.GetCurrentObjective().ToString();
        }

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


    private void lookAtObject()
    {
        Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, pPickup.GetPickupDistance(), pickupMask)|| Physics.Raycast(cameraRay, out hitInfo, pPickup.GetPickupDistance(), lookAtMask))
        {
            if (hitInfo.collider.GetComponent<Renderer>() && useOutline){
                
                //Debug.Log(rend.materials.Length);
                    if (rend)
                    {
                        rend.material = shaderMaterialEmpty;
                    }
                
                rend = hitInfo.collider.GetComponent<Renderer>();
               // if (rend.materials.Length >= 2)
                //{

                rend.material = shaderMaterial;
                    /// rend.materials[1] = shaderMaterial;
                    /// 
                //}
            }
            LookedAtItem.text = hitInfo.transform.name;
            LookedAtItemDesc.text = findLookAtDesc(hitInfo.transform.name);
            OnClickItems onClickItems = hitInfo.collider.GetComponent<OnClickItems>();
            if (onClickItems && Input.GetMouseButtonDown(0))
            {
                onClickItems.Cliked();
            }
            

        }
        else
        {
            LookedAtItem.text = null;
            LookedAtItemDesc.text = null;
            if (rend && useOutline)
            {
                //if (rend.materials.Length >= 2)
                //{
                    rend.material = shaderMaterialEmpty;
                    //rend.materials[1] = shaderMaterialEmpty;
                //}
            }

        }
    }

    private void SetSliderValues()
    {
        sliderOxygen.value = (float)playerLife.GetOxygen()/100;
        sliderHealth.value = (float)playerLife.GetLife()/100;
    }
}
