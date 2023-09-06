using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField]
    FoamBullet foamPrefab;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float shootPower;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {

        }
    }
}
