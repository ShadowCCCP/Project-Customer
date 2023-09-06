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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WaterSource")
        {
            Debug.Log("water in");
            wet.SetActive(true);
            wetBool = true;
        }
    }

    public bool GetWetStatus()
    {
        return wetBool;
    }
}
