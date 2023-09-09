using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    // Start is called before the first frame update

    bool waterOn = false;
    [SerializeField]
    GameObject water;

    //[SerializeField]
    Camera _camera;

    [SerializeField]
    int clickableDistance = 5;
    [SerializeField]
    LayerMask lookAtMask = 8;

    void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        waterCheck();

        Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, clickableDistance,  lookAtMask))
        {
            if(hitInfo.transform.tag == "WaterSource" && Input.GetMouseButtonDown(0))
            {
                 if (!waterOn)
                 {
                     waterOn = true;
                 }
                 else
                 {
                     waterOn = false;
                 }
            }
        }
    }

    void waterCheck()
    {
        if (waterOn)
        {
            water.SetActive(true);
        }
        else
        {
            water.SetActive(false);
        }
    }

    public bool GetWaterStatus()
    {
        return waterOn;
    }
}
