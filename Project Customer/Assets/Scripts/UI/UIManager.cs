using System;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static System.TimeZoneInfo;

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

    public TextMeshProUGUI[] objectives = new TextMeshProUGUI[3];

    PhysicsPickup pPickup;
    [SerializeField]
    Color objectiveFinishedColor = Color.green;
    Color objectiveNormalColor;
    Color currentColor;

    [SerializeField]
    float colorTransitionDuration = 3;
    float[] colorTransitionTimer = new float[3];
    bool[] doOnce = new bool[3];

    [SerializeField]
    float cooldown = 1;
    float activatedAt;

    [SerializeField]
    Transform oxygenBar;

    [SerializeField]
    Transform lifeBar;

    bool[] transition = new bool[3];



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
    private AdvancedObjectivesScript advancedObjectivesScript;

    Camera _camera;

    [SerializeField]
    LayerMask pickupMask;
    [SerializeField]
    LayerMask lookAtMask;
    [SerializeField]
    LayerMask rotatebleOnlyMask;

    [SerializeField]
    bool useOutline = false;

    [SerializeField]
    Material shaderMaterial;
    [SerializeField]
    Material shaderMaterialEmpty;

    Renderer rend;
    Animator anim;
    Life life;

    [SerializeField]
    Slider sliderOxygen;
    [SerializeField]
    Slider sliderHealth;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        life = FindObjectOfType<Life>();

        pPickup = FindObjectOfType<PhysicsPickup>();
        objectiveNormalColor = Objective.color;

        _camera = FindObjectOfType<Camera>();
        objectivesScript = FindObjectOfType<ObjectivesScript>();
        advancedObjectivesScript = FindObjectOfType<AdvancedObjectivesScript>();

        ItemDescription.text = null;
        LookedAtItem.text = null;
        LookedAtItemDesc.text = null;
        ExtraHint.text = null;

        playerPhysicsPickup = FindObjectOfType<PhysicsPickup>();
        playerLife = FindObjectOfType<Life>();
        //ItemName.text = playerPhysicsPickup.objectName;

        LifeText.text = "Life: " + playerLife.GetLife().ToString();
        OxygenLeftText.text = "Oxygen: " + playerLife.GetOxygen().ToString(); ;
        Objective.text = "Objective: " + objectivesScript.GetCurrentObjective().ToString();

        for (int i = 0; i < 3; i++)
        {
            objectives[i].text = advancedObjectivesScript.GetCurrentObjective(i + 1);
        }

        print(advancedObjectivesScript.GetCurrentObjective(1).ToString());

    }

    // Update is called once per frame
    void Update()
    {
        TransitionText();

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

        if(LifeText.text.Substring(LifeText.text.IndexOf(':') + 2) != playerLife.GetLife().ToString())
        {
            LifeText.text = "Life: " + playerLife.GetLife().ToString();
        }
        if (OxygenLeftText.text.Substring(OxygenLeftText.text.IndexOf(':') + 2) != playerLife.GetOxygen().ToString())
        {
            OxygenLeftText.text = "Oxygen: " + playerLife.GetOxygen().ToString();
        }

        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].text != advancedObjectivesScript.GetCurrentObjective(i + 1).ToString() && !doOnce[i])
            {
                transition[i] = true;
            }
        }
    }

    public void ToggleHealthOxygenBar()
    {
        life.damageActivated = !life.damageActivated;
        oxygenBar.gameObject.SetActive(!oxygenBar.gameObject.activeSelf);
        lifeBar.gameObject.SetActive(!lifeBar.gameObject.activeSelf);
    }

    void TransitionText()
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (transition[i])
            {
                ColorTransition(objectives[i], i);
            }

            FinishColorTransition(i);
        }
    }

    void ColorTransition(TextMeshProUGUI text, int transitionIndex)
    {
        if (colorTransitionTimer[transitionIndex] < colorTransitionDuration)
        {
            currentColor = Color.Lerp(objectiveNormalColor, objectiveFinishedColor, colorTransitionTimer[transitionIndex] / colorTransitionDuration);
            text.color = currentColor;
            colorTransitionTimer[transitionIndex] += Time.deltaTime;
        }
        else
        {
            text.color = objectiveFinishedColor;
            transition[transitionIndex] = false;
            doOnce[transitionIndex] = true;
            activatedAt = Time.time;
        }
    }

    void FinishColorTransition(int transitionIndex)
    {
        int count = 0;
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].color == objectiveFinishedColor || objectives[i].text == " ")
            {
                count++;
            }
        }
        
        if(count >= 3 && Time.time - activatedAt > cooldown && doOnce[transitionIndex])
        {
            for (int i = 0; i < objectives.Length; i++)
            {
                objectives[i].color = objectiveNormalColor;
                colorTransitionTimer[i] = 0;
                objectives[i].text = advancedObjectivesScript.GetCurrentObjective(i + 1).ToString();
                doOnce[i] = false;
            }
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
        if (Physics.Raycast(cameraRay, out hitInfo, pPickup.GetPickupDistance(), pickupMask)|| Physics.Raycast(cameraRay, out hitInfo, pPickup.GetPickupDistance(), lookAtMask) || Physics.Raycast(cameraRay, out hitInfo, pPickup.GetPickupDistance(), rotatebleOnlyMask))
        {
            if (hitInfo.transform.tag == "Key" && !playerPhysicsPickup.activatePickupKeys) return;

            if (hitInfo.collider.GetComponent<Renderer>() && useOutline){
                
               if (rend)
               {
                     rend.material = shaderMaterialEmpty;
               }
                
                rend = hitInfo.collider.GetComponent<Renderer>();


                rend.material = shaderMaterial;

            }
            LookedAtItem.text = hitInfo.transform.name;
            LookedAtItemDesc.text = findLookAtDesc(hitInfo.transform.name);
            OnClickItems onClickItems = hitInfo.collider.GetComponent<OnClickItems>();
            
            if (onClickItems && Input.GetMouseButtonDown(0) && Time.timeScale !=0)
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

    public void TriggerSleepAnimation()
    {
        anim.SetTrigger("Sleep");
    }
}
