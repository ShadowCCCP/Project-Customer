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

    // Update is called once per frame
    void Update()
    {
        if (/*Input.GetMouseButtonDown(0) && */ onWaterSource)
        {
            Debug.Log("water in");
            wet.SetActive(true);
            wetBool = true;
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
        Debug.Log("dry");
        wetBool=false;
        wet.gameObject.SetActive(false);
    }

    public bool GetWetStatus()
    {
        return wetBool;
    }

    
}
