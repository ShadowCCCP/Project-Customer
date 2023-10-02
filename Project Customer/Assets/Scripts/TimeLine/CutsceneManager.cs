using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    Transform[] cutscenes;

    CutscenePlayer[] cutscenePlayers = new CutscenePlayer[3];

    int cutsceneCount = 0;

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
        if(Input.GetKeyDown(KeyCode.V))
        {
            TriggerCutscene();
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
