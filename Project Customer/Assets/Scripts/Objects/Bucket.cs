using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject wet;


    bool wetBool = false;

    public bool trueIfBlanket = false;

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
            if (!trueIfBlanket)
            {
               name = "Water Bucket";
            }
            else
            {
                name = "Wet Blanket";
            }
        }
       
    }

    private void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(targetRotationEulerAngles);
        Quaternion currentRotation = transform.rotation;

        if (!trueIfBlanket && Quaternion.Angle(currentRotation, targetRotation) > rotationTolerance)
        {
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreesPerSecond * Time.deltaTime);
            transform.rotation = newRotation;
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
        if (!trueIfBlanket)
        {
            name = "Bucket";
        }
        else
        {
            name = "Blanket";
        }
    }

    public bool GetWetStatus()
    {
        return wetBool;
    }
}
