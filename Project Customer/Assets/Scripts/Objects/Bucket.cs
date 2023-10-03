using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject wet;
    [SerializeField]
    private GameObject potion;


    bool wetBool = false;

    bool trueIfPotion = false;

    void Start()
    {

    }

    // For the bucket rotation...
    Vector3 targetRotationEulerAngles = new Vector3(0, 0, 0);
    float rotationTolerance = 1;

    [SerializeField]
    float maxDegreesPerSecond = 180;


    // Update is called once per frame
    void Update()
    {
        if ( onWaterSource)
        {
            //Debug.Log("water in");
            wet.SetActive(true);
            wetBool = true;
            if (!trueIfPotion)
            {
               name = "Water Bucket";
            }

        }
       
    }

    private void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(targetRotationEulerAngles);
        Quaternion currentRotation = transform.rotation;

        if ( Quaternion.Angle(currentRotation, targetRotation) > rotationTolerance)
        {
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreesPerSecond * Time.deltaTime);
            transform.rotation = newRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FireRepelantPowder" && wetBool)
        {
            name = "Spirit Repelant";
            //potion.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    bool onWaterSource = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WaterSource" )
        {
            Sink sink = other.gameObject.GetComponent<Sink>();
            if (sink.GetWaterStatus())
            {
                onWaterSource = true;
            }

        } else if(other.tag == "Plant" && wetBool == true)
        {
            Invoke("Dry", 1f);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WaterSource")
        {
            onWaterSource = false;

        }
    }

    public void Dry()
    {
        //Debug.Log("dry");
        wetBool=false;
        wet.gameObject.SetActive(false);
        //potion.gameObject.SetActive(false);

        name = "Bucket";
        Debug.Log(gameObject.name);
    }

    public bool GetPotionStatus()
    {
        return trueIfPotion;
    }

    
}
