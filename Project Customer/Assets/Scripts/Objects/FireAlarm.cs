using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour
{
    AudioSource audioSource;
    bool doOnce;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Fire.flameCount > 0 && !doOnce)
        {
            audioSource.Play();
            doOnce = true;
        }
        else if(Fire.flameCount == 0 && doOnce)
        {
            audioSource.Stop();
            doOnce = false;
        }
    }
}
