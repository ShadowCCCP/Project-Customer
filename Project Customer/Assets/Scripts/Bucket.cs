using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject water;

    bool filled = false;

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
            water.SetActive(true);
            filled = true;
        }
    }

    public bool GetFillStatus()
    {
        return filled;
    }
}
