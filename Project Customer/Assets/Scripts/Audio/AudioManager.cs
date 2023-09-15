using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (audioManager == null) audioManager = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
        }
    }

    public void Play(string name)
    {
        // Look inside the "sounds" array and try to find a sound with a matching name...
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("ERROR: " + name + " sound could not be found...");
            return;
        }
        s.source.Play();
    }

    public void ChangeVolume(float volume)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = volume;
        }
    }
}
