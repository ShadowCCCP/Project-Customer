using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEvent : MonoBehaviour
{
    public static event Action onPlayCutscene;

    [SerializeField]
    float timeBeforeCutscenePlays = 2;

    bool firstFlameSpawned;

    [SerializeField]
    float eventTriggerCooldown = 60;
    float lastTiggered;

    private void Update()
    {
        if (Fire.flameCount > 0 && !firstFlameSpawned)
        {
            firstFlameSpawned = true;
            lastTiggered = Time.time;
        }

        if(firstFlameSpawned && Time.time - lastTiggered > eventTriggerCooldown)
        {
            eventTriggerCooldown += 10;
            TriggerEvent();
            lastTiggered = Time.time;
        }
    }

    public void TriggerEventDelayed()
    {
        Invoke("TriggerEvent", timeBeforeCutscenePlays);
    }

    private void TriggerEvent()
    {
        if (onPlayCutscene != null) onPlayCutscene();
    }
}
