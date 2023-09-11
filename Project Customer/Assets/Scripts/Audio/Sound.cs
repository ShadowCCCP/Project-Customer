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

    [Range(0, 1)]
    public float volume;

    [Range(0.1f, 3)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
