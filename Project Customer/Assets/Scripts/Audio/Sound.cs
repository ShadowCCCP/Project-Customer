using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public bool loop;

    public float volume = 1;

    public float pitch = 1;

    [HideInInspector]
    public AudioSource source;
}
