using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArthurTesting : MonoBehaviour
{
    [SerializeField]
    string soundToPlay = "NormalStep1";

    AudioManager audioManager;

    void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            audioManager.Play(soundToPlay);
        }
    }
}
