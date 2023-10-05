using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static event Action allCutscenesPlayed;

    [SerializeField]
    Transform[] cutscenes;

    CutscenePlayer[] cutscenePlayers = new CutscenePlayer[3];

    int cutsceneCount = 0;
    bool doOnce;

    void Start()
    {
        CutsceneEvent.onPlayCutscene += TriggerCutscene;

        for (int i = 0; i < cutscenes.Length; i++)
        {
            cutscenePlayers[i] = cutscenes[i].GetComponent<CutscenePlayer>();
        }
    }

    private void Update()
    {
        if(cutsceneCount >= 3 && !doOnce && allCutscenesPlayed != null)
        {
            allCutscenesPlayed();
            doOnce = true;
        }
    }

    void OnDestroy()
    {
        CutsceneEvent.onPlayCutscene -= TriggerCutscene;
    }

    private void TriggerCutscene()
    {
        if(cutsceneCount < cutscenes.Length)
        {
            cutscenePlayers[cutsceneCount].PlayCutscene();
        }

        cutsceneCount++;
    }
}
