using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        OnClickItems.onBedClicked += TriggerLightTemperature;
    }

    void OnDestroy()
    {
        OnClickItems.onBedClicked -= TriggerLightTemperature;
    }

    private void TriggerLightTemperature()
    {
        Invoke("SwitchLightTemperature", 4);
    }

    private void SwitchLightTemperature()
    {
        lightComponent.colorTemperature = 1900;
    }
}